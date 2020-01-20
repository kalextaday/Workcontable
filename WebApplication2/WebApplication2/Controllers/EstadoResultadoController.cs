using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.TableViewModels;
using System.Data.Entity;

namespace WebApplication2.Controllers
{
    public class EstadoResultadoController : Controller
    {
        USUARIO sesion_Usuario = null;

        // GET: EstadoResultado
        public ActionResult Index()
        {
            sesion_Usuario = (USUARIO)Session["USUARIO"];

            // *** Carga de fechas iniciales a la vista ***
            List<LBTableViewModel> lst_f_inicio = null;
            using (Entities db = new Entities())
            {
                lst_f_inicio = (from d in db.LIBRO_CONTABLE
                                where d.USU_ID == sesion_Usuario.ID_USUARIO
                                select new LBTableViewModel
                                {
                                    id_lib_contable = d.ID_LIB_CONTABLE,
                                    fecha_inicio = d.FECHA_INICIO,
                                    fecha_fin = d.FECHA_FIN,
                                    total_ingresos = d.TOTAL_INGRESOS,
                                    total_gastos = d.TOTAL_GASTOS
                                }).ToList();
            }

            //Carga de las fechas de inicio al ViewBag
            List<SelectListItem> opc_fecha_inicio = new List<SelectListItem>();

            opc_fecha_inicio.Add(new SelectListItem { Text = "Seleccione una fecha", Value = "" });

            foreach (var opc in lst_f_inicio)
            {

                opc_fecha_inicio.Add(new SelectListItem { Text = opc.fecha_inicio.ToString("dd MMMM, yyyy"), Value = opc.fecha_inicio.ToString("yyyyMMdd") });
            }

            ViewBag.opc_fecha_inicio = opc_fecha_inicio;
            ///////////////


            return View();
        }

        //Get: Creacion del estado de resultado
        public PartialViewResult SectionStateResult(string id)
        {
            sesion_Usuario = (USUARIO)Session["USUARIO"];

            //Cambio de string fecha a string fecha con formato
            string fecha_inicio = DateTime.ParseExact(id, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd"); ;
            

            //Cambio de string a datetime
            DateTime id_fecha_inicio = Convert.ToDateTime(fecha_inicio);

            EstadoResulTableViewModel oEstadoResult = new EstadoResulTableViewModel();
            decimal varImpuesto = 0;
            //Obtengo el libro contable que solicitan por la fecha
            LIBRO_CONTABLE oLib;

            using (Entities db = new Entities())
            {
                var oLst_Lib = from d in db.LIBRO_CONTABLE
                               where d.USU_ID == sesion_Usuario.ID_USUARIO &&
                               DbFunctions.TruncateTime(d.FECHA_INICIO) == DbFunctions.TruncateTime(id_fecha_inicio) //DbFunctions: clase de .net para el datetime
                               select d;

                oLib = oLst_Lib.First();
            }

            

            //Obtengo todas las transacciones del libro contable solicitado
            List<DetLBTableViewModel> lst_RegDet = null;
            using (Entities db = new Entities())
            {
                lst_RegDet = (from d in db.REG_DET_LIBROS
                              where d.LIB_ID == oLib.ID_LIB_CONTABLE
                               select new DetLBTableViewModel
                               {
                                   id_reg_det_lib = d.ID_REG_DET_LIB,
                                   tra_id = d.TRA_ID,
                                   lib_id = d.LIB_ID
                               }).ToList();
            }

            //Obtengo cada transaccion del libro contable solicitado para calcular el impuesto

            using (Entities db = new Entities())
            {
                foreach (var item in lst_RegDet)
                {
                    var oTrans = db.TRANSACCION_REAL.Find(item.tra_id);

                    varImpuesto = varImpuesto + oTrans.IMPUESTO;
                }
            }

            //Lleno el modelo para mandarlo a la vista parcial
            oEstadoResult.lib_contable_id = oLib.ID_LIB_CONTABLE;
            oEstadoResult.ingresos = oLib.TOTAL_INGRESOS;
            oEstadoResult.utilidad_bruta = oLib.TOTAL_INGRESOS;
            oEstadoResult.gastos = oLib.TOTAL_GASTOS;
            oEstadoResult.utilidad_antes_imp = oLib.TOTAL_INGRESOS - oLib.TOTAL_GASTOS;
            oEstadoResult.impuestos = varImpuesto;
            oEstadoResult.utilidad_ejercicio = (oLib.TOTAL_INGRESOS - oLib.TOTAL_GASTOS) - varImpuesto;

            oEstadoResult.fecha_inicio = oLib.FECHA_INICIO;
            oEstadoResult.fecha_fin = oLib.FECHA_FIN;



            return PartialView("_SectionStateResult", oEstadoResult);
        }
    }
}