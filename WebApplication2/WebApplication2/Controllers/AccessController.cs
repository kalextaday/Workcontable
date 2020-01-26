using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Filters;

namespace WebApplication2.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }
        
        //los parametros tienen que llamarse igual como este en la vista
        public ActionResult Enter(string user, string password)
        {
            try
            {
                password = Encryption.GetSHA256(password);
                using (Entities db=new Entities())
                {
                    var lst = from d in db.USUARIO
                              where d.NOMBRE_USUARIO == user && d.PASSWORD == password
                              select d;
                    if (lst.Count()>0)
                    {
                        //Sesiones
                        USUARIO oUser = lst.First();
                        Session["USUARIO"] = oUser;
                        return Content("1");
                    }
                    else
                    {
                        return Content("Usuario invalido");
                    }
                }

                
            }
            catch (Exception ex) {
                //content es una funcion que regresa un string en ves de vista
                return Content("Ocurrio un error " +ex.Message);
            }
            //return View();
        }


        [HttpGet]
        public ActionResult ForgotPass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPass(string user, string email)
        {

            return View();
        }

    }
}