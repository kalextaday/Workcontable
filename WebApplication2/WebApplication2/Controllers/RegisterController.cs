using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Filters;
using WebApplication2.Models;
using WebApplication2.Models.TableViewModels;
using WebApplication2.Models.ViewModels;
using NLog;
using System.Net.Mail;

namespace WebApplication2.Controllers
{
    public class RegisterController : Controller
    {

        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();


        // GET: Register Usuario
        #region
        public ActionResult Index()
        {
            //******* CARGA DE ROLES EN LA VISTA ********
            List<RolTableViewModel> roles = null;
            using (Entities db = new Entities())
            {
                roles = (from d in db.ROL
                         select new RolTableViewModel
                         {
                             id_rol = d.ID_ROL,
                             nombre = d.NOMBRE
                         }).ToList();
            }

            List<SelectListItem> opc_rol = new List<SelectListItem>();

            foreach (var opc in roles)
            {
                opc_rol.Add(new SelectListItem { Text = opc.nombre, Value = Convert.ToString(opc.id_rol) });
            }

            ViewBag.opc_rol = opc_rol;
            //////////////////////////

            return View();
        }
        #endregion


        //*** AÑADIR ***
        // POST: Usuario
        #region
        [HttpPost]
        public ActionResult Add(UsuExternoViewModel model)
        {
            //return RedirectToAction("Add","Usuario").Error("hasta q hora te espero");

            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                //*** ENVIO DE CORREO ***
                MailMessage correo = new MailMessage();
                string asunto = "Verificacion de usuario - WORKCONTABLE";
                string mensaje = "Felicitaciones !!, tu cuenta ha sido creada correctamente";

                correo.From = new MailAddress("kalexander031098@outlook.com");
                correo.To.Add(model.email);
                correo.Subject = asunto;
                correo.Body = mensaje;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;
                //Configuracion del servidor smtp
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.live.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                string sCuentaCorreo = "kalexander031098@outlook.com";
                string sPasswordCorreo = "wpsconect2015";
                smtp.Credentials = new System.Net.NetworkCredential(sCuentaCorreo, sPasswordCorreo);

                smtp.Send(correo);
                ViewBag.mensaje = "Correo enviado correctamente";


                //////////////

                //*** CREACION DEL USUARIO ***
                using (var db = new Entities())
                {
                    //Creacion objeto USUARIO para insercion en la tabla USUARIO
                    USUARIO oUser = new USUARIO();
                    oUser.NOMBRE_USUARIO = model.nombre_usuario;
                    oUser.PASSWORD = Encryption.GetSHA256(model.password);
                    oUser.ROL_ID = Convert.ToByte(model.rol_id);
                    oUser.NOMBRE = model.nombre;
                    oUser.APELLIDO = model.apellido;
                    oUser.CEDULA = model.cedula;
                    oUser.RUC = model.ruc;
                    oUser.DIRECCION = model.direccion;
                    oUser.TELEFONO = model.telefono;
                    oUser.EMAIL = model.email;

                    db.USUARIO.Add(oUser); //Linkq del insert
                    db.SaveChanges(); //Linkq del commit


                    //Insercion en la tabla ROL_OPERACION
                    int id_User = oUser.ID_USUARIO; //Obtener el id del ultimo registro 
                    int operacion_id;
                    //Rol operacion por defecto respectivo al rol
                    if (oUser.ROL_ID == 1) //Si es admin
                    {
                        operacion_id = 3;
                    }
                    else // Si es usuario o invitado
                    {
                        operacion_id = 7;
                    }

                    try
                    {
                        using (var dbT_RolOperacion = new Entities())
                        {
                            ROL_OPERACION oRolOperacion = new ROL_OPERACION();
                            oRolOperacion.USUARIO_ID = id_User;
                            oRolOperacion.OPERACION_ID = operacion_id;
                            oRolOperacion.ESTADO = true;

                            dbT_RolOperacion.ROL_OPERACION.Add(oRolOperacion);
                            dbT_RolOperacion.SaveChanges();
                        }

                        //return Redirect(Url.Action("Index", "Usuario"));
                    }
                    catch (DbUpdateException e)
                    {
                        //Analisis del error
                        var causa = analizeCaseError(e, "causa");
                        var consecuencia = analizeCaseError(e, "consecuencia");
                        _logger.Info("Causa: " + causa.ToString() + "\nConsecuencia: " + consecuencia.ToString());


                        //Eliminar el ultimo usuario
                        var oLastUser = db.USUARIO.Find(id_User);//Linkq del delete

                        db.USUARIO.Remove(oUser);
                        db.SaveChanges();

                        
                        //return Redirect(Url.Action("Add", "Usuario"));
                    }

                }
            }
            catch (DbUpdateException e)
            {
                //Analisis del error
                var causa = analizeCaseError(e, "causa");
                var consecuencia = analizeCaseError(e, "consecuencia");
                _logger.Info("Causa: " + causa.ToString() + "\nConsecuencia: " + consecuencia.ToString());

                //return Redirect(Url.Action("Add", "Usuario"));
            }
            return Redirect(Url.Action("Index", "General"));
        }
        #endregion


        //*** ANALISIS DE EXCEPCIONES ***
        #region
        public StringBuilder analizeCaseError(DbUpdateException e, string suceso)
        {
            var analisis = new StringBuilder();

            if (suceso.Equals("causa"))
            {
                analisis.AppendLine($"DbUpdateException detalle: {e?.InnerException?.InnerException?.Message}");
            }
            else
            {
                foreach (var eve in e.Entries)
                {
                    analisis.AppendLine($"Entidad: {eve.Entity.GetType().Name} en estado {eve.State} no pudo ser actualizada");
                }
            }

            return analisis;
        }
        #endregion
    }
}