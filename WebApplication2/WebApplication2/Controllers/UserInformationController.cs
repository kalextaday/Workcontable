using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.ViewModels;

namespace WebApplication2.Controllers
{
    public class UserInformationController : Controller
    {

        USUARIO sesion_Usuario = null;

        
        // GET: UserInformation
        public ActionResult Perfil()
        {
            sesion_Usuario = (USUARIO)Session["USUARIO"];

            UserInfoViewModel oUserInfo = new UserInfoViewModel();

            var id_rol = sesion_Usuario.ROL_ID;
            string rol_nombre = "";
            //validacion roles
            if (id_rol == 1)
            {
                rol_nombre = "Administrador";
            }
            else if (id_rol == 2)
            {
                rol_nombre = "Usuario";
            }
            else
            {
                rol_nombre = "Visitante";
            }

            //Obteniendo Informacion basica
            oUserInfo.id_usuario = sesion_Usuario.ID_USUARIO;
            oUserInfo.nombre_usuario = sesion_Usuario.NOMBRE_USUARIO;
            oUserInfo.password = sesion_Usuario.PASSWORD;
            oUserInfo.rol_id = sesion_Usuario.ROL_ID;
            oUserInfo.rol_nombre = rol_nombre;
            oUserInfo.nombre = sesion_Usuario.NOMBRE;
            oUserInfo.apellido = sesion_Usuario.APELLIDO;
            oUserInfo.cedula = sesion_Usuario.CEDULA;
            oUserInfo.ruc = sesion_Usuario.RUC;
            oUserInfo.direccion = sesion_Usuario.DIRECCION;
            oUserInfo.telefono = sesion_Usuario.TELEFONO;
            oUserInfo.email = sesion_Usuario.EMAIL;

            //Obteniendo informacion extra: idioma
            List<IdiomaViewModel> lst = null;
            using (Entities db = new Entities())
            {
                lst = (from d in db.IDIOMA
                       where d.USU_ID == sesion_Usuario.ID_USUARIO
                       select new IdiomaViewModel
                       {
                           id_idioma = d.ID_IDIOMA,
                           usu_id = d.USU_ID,
                           nombre=d.NOMBRE
                       }).ToList();
            }
            oUserInfo.lstIdiomas = lst;

            return View(oUserInfo);
        }

    }
}