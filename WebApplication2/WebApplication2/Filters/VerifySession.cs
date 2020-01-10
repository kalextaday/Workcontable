using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Controllers;
using WebApplication2.Models;

namespace WebApplication2.Filters
{
    public class VerifySession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var oUser = (USUARIO)HttpContext.Current.Session["USUARIO"];

            //evalua si el objeto es nulo
            if (oUser == null)
            {
                //evalua si es diferente al access controller
                if(filterContext.Controller is AccessController ==false){
                    filterContext.HttpContext.Response.Redirect("~/Access/Index");
                }
            }
            else
            {
                if (filterContext.Controller is AccessController == true)
                {
                    filterContext.HttpContext.Response.Redirect("~/Home/Index");
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}