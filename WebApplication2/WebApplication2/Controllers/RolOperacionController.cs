using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.TableViewModels;
using WebApplication2.Models.ViewModels;
using WebApplication2.Filters;

namespace WebApplication2.Controllers
{
    public class RolOperacionController : Controller
    {
        
        // GET: RolOperacion
        public ActionResult Index(int id)
        {
            USUARIO sesion_Usuario = null;

            ViewBag.id_usuario= id;

            using (var db = new Entities())
            {
                sesion_Usuario = db.USUARIO.Find(id);
            }


            List<PermisoTableViewModel> lstOpera = null;
            using (Entities db = new Entities())
            {
                lstOpera = (from d in db.ROL_OPERACION
                       where d.USUARIO_ID == sesion_Usuario.ID_USUARIO
                       select new PermisoTableViewModel
                       {
                           id_rol_operacion = d.ID_ROL_OPERACION,
                           usuario_id = d.USUARIO_ID,
                           operacion_id = d.OPERACION_ID,
                           estado = d.ESTADO
                       }).ToList();
            }

            //List<RolOperaTableViewModel> lstOpera = new List<RolOperaTableViewModel>();
            using (var db = new Entities())
            {
                foreach (var oPermiso in lstOpera)
                {
                    var oUsuario = db.USUARIO.Find(oPermiso.usuario_id);
                    var oRol = db.ROL.Find(oUsuario.ROL_ID);
                    var oOperacion = db.OPERACION.Find(oPermiso.operacion_id);
                    var oModulo = db.MODULO.Find(oOperacion.MODULO_ID);

                    oPermiso.rol_nombre = oRol.NOMBRE;
                    oPermiso.operacion_nombre = oOperacion.NOMBRE;
                    oPermiso.modulo_nombre = getNombreDelModulo(oModulo.ID_MODULO);
                }
            }

            return View(lstOpera);
        }

        public string getNombreDelModulo(int idModulo)
        {
            Entities db = new Entities();
            var modulo = from m in db.MODULO
                         where m.ID_MODULO == idModulo
                         select m.NOMBRE;
            String nombreModulo;
            try
            {
                nombreModulo = modulo.First();
            }
            catch (Exception)
            {
                nombreModulo = "";
            }
            return nombreModulo;
        }


        //*** EDITAR ***
        // GET: Permisos
        public ActionResult Edit(int id)
        {

            List<UsuarioTableViewModel> lst_usuarios = null;
            using (Entities db = new Entities())
            {
                lst_usuarios = (from d in db.USUARIO
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

            List<SelectListItem> opc_user = new List<SelectListItem>();

            foreach (var opc in lst_usuarios)
            {
                opc_user.Add(new SelectListItem { Text = opc.nombre_usuario, Value = Convert.ToString(opc.id_usuario) });
            }

            ViewBag.opc_user = opc_user;
            ///////////////






            USUARIO sesion_Usuario = null;

            using (var db = new Entities())
            {
                sesion_Usuario = db.USUARIO.Find(id);
            }


            List<PermisoTableViewModel> lstOpera = null;
            using (Entities db = new Entities())
            {
                lstOpera = (from d in db.ROL_OPERACION
                            where d.USUARIO_ID == sesion_Usuario.ID_USUARIO
                            select new PermisoTableViewModel
                            {
                                id_rol_operacion = d.ID_ROL_OPERACION,
                                usuario_id = d.USUARIO_ID,
                                operacion_id = d.OPERACION_ID,
                                estado = d.ESTADO
                            }).ToList();
            }

            //List<RolOperaTableViewModel> lstOpera = new List<RolOperaTableViewModel>();
            using (var db = new Entities())
            {
                foreach (var oPermiso in lstOpera)
                {
                    var oUsuario = db.USUARIO.Find(oPermiso.usuario_id);
                    var oRol = db.ROL.Find(oUsuario.ROL_ID);
                    var oOperacion = db.OPERACION.Find(oPermiso.operacion_id);
                    var oModulo = db.MODULO.Find(oOperacion.MODULO_ID);

                    oPermiso.rol_nombre = oRol.NOMBRE;
                    oPermiso.operacion_nombre = oOperacion.NOMBRE;
                    oPermiso.modulo_nombre = getNombreDelModulo(oModulo.ID_MODULO);
                }
            }





            return View(lstOpera);
        }

        //*** OTORGAR ***
        // GET: Dar Permisos

        public ActionResult Grant(int id, int usu_dest_id)
        {
            /*
            if (!ModelState.IsValid)
            {
                return View();
            }*/

            using (var db = new Entities())
            {
                ROL_OPERACION oRolOpeUser = new ROL_OPERACION();

                var oRolOpe = db.ROL_OPERACION.Find(id);

                oRolOpeUser.USUARIO_ID = usu_dest_id;
                oRolOpeUser.OPERACION_ID = oRolOpe.OPERACION_ID;

                db.ROL_OPERACION.Add(oRolOpeUser);
                db.SaveChanges();
            }

                return Content("1");
        }

        //*** PERMISOS ***
        // GET: Pedir Permisos de un usuario especifco
        public ActionResult GetPermiso(int usuario_id)
        {

            List<PermisoTableViewModel> lst_RolOperacion = null;
            using (Entities db = new Entities())
            {
                lst_RolOperacion = (from d in db.ROL_OPERACION
                                    where d.USUARIO_ID== usuario_id
                                    select new PermisoTableViewModel
                                    {
                                        id_rol_operacion = d.ID_ROL_OPERACION
                                    }).ToList();
            }


            return Content("1");
        }
    }
}