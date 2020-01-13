using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.TableViewModels;
using WebApplication2.Models.ViewModels;
using WebApplication2.Filters;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Data;
using OfficeOpenXml;
using NLog;

namespace WebApplication2.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger _logger= LogManager.GetCurrentClassLogger();

        USUARIO sesion_Usuario = null;  

        // GET: Usuario
        //[AuthorizeUser(new int[1] {3})] //dar autorizacion al usuario de este action result
        public ActionResult Index()
        {
            //LOGGER//
            _logger.Info("Comenzo el metodo index");
            ////


            sesion_Usuario = (USUARIO)Session["USUARIO"];

            ViewBag.rol = sesion_Usuario.ROL_ID;

            List<UsuarioTableViewModel> lst = null;
            using (Entities db = new Entities())
            {
                lst = (from d in db.USUARIO
                       select new UsuarioTableViewModel
                       {
                           id_usuario=d.ID_USUARIO,
                           nombre_usuario = d.NOMBRE_USUARIO,
                           rol_id = d.ROL_ID,
                           nombre = d.NOMBRE,
                           apellido = d.APELLIDO,
                           cedula = d.CEDULA,
                           ruc = d.RUC,
                           direccion = d.DIRECCION,
                           telefono = d.TELEFONO,
                           email = d.EMAIL
                       }).ToList();
            }

            

            return View(lst);
        }

        //*** EXPORTAR ***
        // GET: Tabla
        public void Export()
        {
            List<UsuarioTableViewModel> lst = null;
            using (Entities db = new Entities())
            {
                lst = (from d in db.USUARIO
                       select new UsuarioTableViewModel
                       {
                           id_usuario = d.ID_USUARIO,
                           nombre_usuario = d.NOMBRE_USUARIO,
                           rol_id = d.ROL_ID,
                           nombre = d.NOMBRE,
                           apellido = d.APELLIDO,
                           cedula = d.CEDULA,
                           ruc = d.RUC,
                           direccion = d.DIRECCION,
                           telefono = d.TELEFONO,
                           email = d.EMAIL
                       }).ToList();
            }

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "USUARIO ID";
            ws.Cells["B1"].Value = "NAME USER";
            ws.Cells["C1"].Value = "ROL ID";
            ws.Cells["D1"].Value = "NOMBRE";
            ws.Cells["E1"].Value = "APELLIDO";
            ws.Cells["F1"].Value = "CEDULA";
            ws.Cells["G1"].Value = "RUC";
            ws.Cells["H1"].Value = "DIRECCION";
            ws.Cells["I1"].Value = "TELEFONO";
            ws.Cells["J1"].Value = "EMAIL";

            int rowStart = 2;
            foreach (var item in lst)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.id_usuario;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.nombre_usuario;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.rol_id;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.nombre;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.apellido;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.cedula;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.ruc;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.direccion;
                ws.Cells[string.Format("I{0}", rowStart)].Value = item.telefono;
                ws.Cells[string.Format("j{0}", rowStart)].Value = item.email;

                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }


        [HttpGet]
        public ActionResult Add()
        {
            sesion_Usuario = (USUARIO)Session["USUARIO"];

            ViewBag.rol = sesion_Usuario.ROL_ID;

            //******* CARGA DE ROLES EN LA VISTA ********
            List<RolTableViewModel> roles = null;
            using (Entities db = new Entities())
            {
                roles = (from d in db.ROL
                       select new RolTableViewModel
                       {
                           id_rol=d.ID_ROL,
                           nombre=d.NOMBRE
                       }).ToList();
            }

            List<SelectListItem> opc_rol = new List<SelectListItem>();

            foreach (var opc in roles)
            {
                opc_rol.Add(new SelectListItem { Text = opc.nombre,Value=Convert.ToString(opc.id_rol)});
            }

            ViewBag.opc_rol = opc_rol;
            //////////////////////////


            return View();
        }

        [HttpPost]
        public ActionResult Add(UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                using (var db = new Entities())
                {

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

                    db.USUARIO.Add(oUser);
                    db.SaveChanges();
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entidad de tipo \"{0}\" en estado \"{1}\" tiene los siguientes errores:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Propiedad: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }


            return Redirect(Url.Content("~/Usuario/"));
        }


        //*** EDITAR ***
        // GET: Usuario
        public ActionResult Edit(int id)
        {
            sesion_Usuario = (USUARIO)Session["USUARIO"];

            ViewBag.rol = sesion_Usuario.ROL_ID;

            // CARGA DE ROLES EN LA VISTA 
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




            EditUsuarioViewModel model = new EditUsuarioViewModel();

            using (var db=new Entities())
            {
                var oUser = db.USUARIO.Find(id);

                model.id_usuario = oUser.ID_USUARIO;
                model.nombre_usuario = oUser.NOMBRE_USUARIO;
                model.password = oUser.PASSWORD;

                var rol = oUser.ROL_ID;
                //validacion roles
                if (rol == 1)
                {
                    model.rol_id = "Administrador";
                }
                else if (rol == 2)
                {
                    model.rol_id = "Usuario";
                }
                else
                {
                    model.rol_id = "Visitante";
                }


                model.nombre = oUser.NOMBRE;
                model.apellido = oUser.APELLIDO;
                model.cedula = oUser.CEDULA;
                model.ruc = oUser.RUC;
                model.direccion = oUser.DIRECCION;
                model.telefono = oUser.TELEFONO;
                model.email = oUser.EMAIL;

                
            }

            return View(model);
        }

        //*** EDITAR ***
        // POST: Usuario
        [HttpPost]
        public ActionResult Edit(EditUsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            using (var db = new Entities())
            {
                var oUser = db.USUARIO.Find(model.id_usuario);

                oUser.NOMBRE_USUARIO=model.nombre_usuario;
                oUser.ROL_ID= Convert.ToByte(model.rol_id);
                oUser.NOMBRE=model.nombre;
                oUser.APELLIDO=model.apellido;
                oUser.CEDULA=model.cedula;
                oUser.RUC=model.ruc;
                oUser.DIRECCION=model.direccion;
                oUser.TELEFONO=model.telefono;
                oUser.EMAIL=model.email;

                if (model.password != null && model.password.Trim() != "")
                {
                    oUser.PASSWORD = Encryption.GetSHA256(model.password);
                }

                db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }

            return Redirect(Url.Content("~/Usuario/"));
        }


        //*** ELIMINAR ***
        // POST: Usuario
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var db = new Entities())
            {
                var oUser = db.USUARIO.Find(id);

                db.USUARIO.Remove(oUser);
                //db.Entry(oUser).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();

            }

            return Content("1");
        }

    }

}