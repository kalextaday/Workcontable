using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Controllers;
using WebApplication2.Models;

namespace WebApplication2.Filters
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple=false)]
    public class AuthorizeUser : AuthorizeAttribute
    {
        private USUARIO oUsuario;
        private Entities db = new Entities();
        private int[] id_operacion;

        public AuthorizeUser(int[] id_operacion)
        {
            this.id_operacion = id_operacion;
        }


        public override void OnAuthorization(AuthorizationContext filterContext)
        {/*
            String nombreOperacion = "";
            String nombreModulo = "";
            try
            {
                oUsuario = (USUARIO)HttpContext.Current.Session["USUARIO"];
                
                var lstMisOperaciones = from m in db.ROL_OPERACION
                                        where m.ROL_ID == oUsuario.ROL_ID 
                                        && id_operacion.Contains(m.OPERACION_ID)
                                        select m;


                /*
                var lstMisOperaciones = from m in db.ROL_OPERACION
                                        where m.ROL_ID == oUsuario.ROL_ID
                                            && m.OPERACION_ID == vid_operacion
                                        select m;
                                        9
            
                var id_opera = lstMisOperaciones.First();

                if (lstMisOperaciones.ToList().Count() == 0)
                {
                    var oOperacion = db.OPERACION.Find(id_opera);
                    int? idModulo = oOperacion.MODULO_ID;
                    nombreOperacion = getNombreDeOperacion(Convert.ToInt16(id_opera));
                    nombreModulo = getNombreDelModulo(idModulo);
                    filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + nombreOperacion + "&modulo=" + nombreModulo + "&msjeErrorExcepcion=");
                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + nombreOperacion + "&modulo=" + nombreModulo + "&msjeErrorExcepcion=" + ex.Message);
            }
            */
        }

        public string getNombreDeOperacion(int idOperacion)
        {
            var ope = from op in db.OPERACION
                      where op.ID_OPERACION == idOperacion
                      select op.NOMBRE;
            String nombreOperacion;
            try
            {
                nombreOperacion = ope.First();
            }
            catch (Exception)
            {
                nombreOperacion = "";
            }
            return nombreOperacion;
        }

        public string getNombreDelModulo(int? idModulo)
        {
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
    }
}