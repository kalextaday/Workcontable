using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Controllers;
using WebApplication2.Models;

namespace WebApplication2.Filters
{
    public class VerifyAccess : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            /*
            var oUser = (USUARIO)HttpContext.Current.Session["USUARIO"]; //Obtengo los datos de la sesion

            if (filterContext.Controller is GeneralController == false && oUser == null)
            {
                if(filterContext.Controller is AccessController == false)
                {
                    filterContext.HttpContext.Response.Redirect("~/General/Index");
                }
            }
            
            var oUser = (USUARIO)HttpContext.Current.Session["USUARIO"]; //Obtengo los datos de la sesion

            //evalua si el objeto es nulo
            if (oUser == null)
            {
                //evalua el acceso al controller
                if (filterContext.Controller is GeneralController == false)
                {
                    filterContext.HttpContext.Response.Redirect("~/General/Index");
                }
            }
            else
            {
                if (filterContext.Controller is AccessController == true)
                {
                    filterContext.HttpContext.Response.Redirect("~/Home/Index");
                }
            }*/


                base.OnActionExecuting(filterContext);
        }
    }
}