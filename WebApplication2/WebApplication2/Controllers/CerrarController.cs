using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class CerrarController : Controller
    {

        public ActionResult Logoff()
        {
            Session["USUARIO"] = null;

            return RedirectToAction("Index","Access");
        }
    }
}