using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Filters;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        USUARIO sesion_Usuario = null;

        public ActionResult Index()
        {
            
            sesion_Usuario = (USUARIO)Session["USUARIO"];

            ViewBag.rol = sesion_Usuario.ROL_ID;
            ViewBag.username = sesion_Usuario.NOMBRE_USUARIO;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}