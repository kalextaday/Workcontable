using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.TableViewModels;

namespace WebApplication2.Controllers
{
    public class ComparativoController : Controller
    {
        USUARIO sesion_Usuario = null;

        // GET: Comparativo
        public ActionResult Resultados()
        {

            sesion_Usuario = (USUARIO)Session["USUARIO"];

            // *** Carga de fechas iniciales a la vista ***
            List<PresuTableViewModel> lst_f_inicio = null;
            using (Entities db = new Entities())
            {
                lst_f_inicio = (from d in db.PRESUPUESTO
                       where d.USU_ID == sesion_Usuario.ID_USUARIO
                       select new PresuTableViewModel
                       {
                           id_presupuesto = d.ID_PRESUPUESTO,
                           fecha_inicio = d.FECHA_INICIO,
                           fecha_fin = d.FECHA_FIN,
                           valor_pre = d.VALOR_PRE,
                           total_gastos = d.TOTAL_GASTOS
                       }).ToList();
            }

            //Carga de las fechas de inicio al ViewBag
            List<SelectListItem> opc_fecha_inicio = new List<SelectListItem>();
            
            foreach (var opc in lst_f_inicio)
            {

                opc_fecha_inicio.Add(new SelectListItem { Text = opc.fecha_inicio.ToString("dd MMMM, yyyy"), Value = opc.fecha_inicio.ToString("yyyyMMdd") });
            }

            ViewBag.opc_fecha_inicio = opc_fecha_inicio;

            //Carga de las fechas de finalizacion al ViewBag
            List<SelectListItem> opc_fecha_fin = new List<SelectListItem>();

            foreach (var opc in lst_f_inicio)
            {
                opc_fecha_fin.Add(new SelectListItem { Text = opc.fecha_fin.ToString("dd MMMM, yyyy"), Value = opc.fecha_fin.ToString("yyyyMMdd") });
            }

            ViewBag.opc_fecha_fin = opc_fecha_fin;
            ///////////////

            return View();
        }


        // GET: Mostrar tabla comparativa con varianza
        public PartialViewResult SectionCompare(string id, string id_f_fin)
        {
            sesion_Usuario = (USUARIO)Session["USUARIO"];

            //Cambio de string fecha a string fecha con formato
            string fecha_inicio=DateTime.ParseExact(id, "yyyyMMdd",CultureInfo.InvariantCulture).ToString("yyyy/MM/dd"); ;
            string fecha_fin= DateTime.ParseExact(id_f_fin, "yyyyMMdd",CultureInfo.InvariantCulture).ToString("yyyy/MM/dd"); ;

            //Cambio de string a datetime
            DateTime id_fecha_inicio = Convert.ToDateTime(fecha_inicio);
            DateTime id_fecha_fin = Convert.ToDateTime(fecha_fin);


            List<ComparativoTableViewModel> lst = null;

            using (Entities db = new Entities())
            {
                lst = (from d in db.PRESUPUESTO
                       join f in db.LIBRO_CONTABLE on d.USU_ID equals f.USU_ID
                       where d.USU_ID == sesion_Usuario.ID_USUARIO &&
                       DbFunctions.TruncateTime(d.FECHA_INICIO) >= DbFunctions.TruncateTime(id_fecha_inicio) && //DbFunctions: clase de .net para el datetime
                       DbFunctions.TruncateTime(d.FECHA_FIN) <= DbFunctions.TruncateTime(id_fecha_fin)          //TruncateTime (): elimina el tiempo
                       select new ComparativoTableViewModel
                       {
                           usu_id = d.USU_ID,

                           id_presupuesto = d.ID_PRESUPUESTO,
                           fecha_inicio_pre = d.FECHA_INICIO,
                           fecha_fin_pre = d.FECHA_FIN,
                           valor_pre = d.VALOR_PRE,
                           total_gastos_pre = d.TOTAL_GASTOS,

                           id_lib_contable = f.ID_LIB_CONTABLE,
                           fecha_inicio_lib = f.FECHA_INICIO,
                           fecha_fin_lib = f.FECHA_FIN,
                           total_ingresos = f.TOTAL_INGRESOS,
                           total_gastos_lib = f.TOTAL_GASTOS,

                           varianza = 0
                       }).ToList();

                //Actualizo el valor de la varianza por el valor correcto
                foreach(var item in lst)
                {
                    item.varianza = getVarianza(item.valor_pre,item.total_gastos_pre,item.total_ingresos,item.total_gastos_lib);
                }
            }


            return PartialView("_Compare",lst);
        }

        //CALCULO DE LA VARIANZA
        //Get
        public decimal getVarianza(decimal valorPre,decimal tGastosPre,decimal tIngLib,decimal tGastosLib)
        {
            decimal diferencia_Pre= valorPre- tGastosPre;
            decimal diferencia_Lib= tIngLib- tGastosLib;
            decimal valorBase;
            decimal varianza;

            if(diferencia_Pre>= diferencia_Lib)
            {
                valorBase = diferencia_Pre;
                varianza = valorBase + diferencia_Lib;
            }
            else
            {
                valorBase = diferencia_Lib;
                varianza = valorBase + diferencia_Pre;
            }

            return varianza;
        }


    }
}