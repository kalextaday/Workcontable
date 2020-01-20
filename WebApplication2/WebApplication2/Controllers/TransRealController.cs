using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.TableViewModels;
using WebApplication2.Models.ViewModels;
using WebApplication2.Filters;
using System.Data.Entity.Validation;
using NLog;
using System.Data.Entity.Infrastructure;
using System.Text;

namespace WebApplication2.Controllers
{
    public class TransRealController : Controller
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        USUARIO sesion_Usuario = null;

        //*** AÑADIR ***
        // GET: TransReal
        #region
        public ActionResult Add(int id)
        {
            ViewBag.id_lb = id;

            //******* CARGA DE RUBROS EN LA VISTA ********
            List<RubroTableViewModel> rubros = null;
            using (Entities db = new Entities())
            {
                rubros = (from d in db.RUBRO
                          select new RubroTableViewModel
                          {
                              id_rubro = d.ID_RUBRO,
                              nombre = d.NOMBRE
                          }).ToList();
            }

            List<SelectListItem> opc_rubro = new List<SelectListItem>();

            foreach (var opc in rubros)
            {
                opc_rubro.Add(new SelectListItem { Text = opc.nombre, Value = Convert.ToString(opc.id_rubro) });
            }

            ViewBag.opc_rubro = opc_rubro;
            //////////////////////////


            List<SelectListItem> opc_tipo = new List<SelectListItem>();
            opc_tipo.Add(new SelectListItem { Text = "Ingreso", Value = "true" });
            opc_tipo.Add(new SelectListItem { Text = "Gasto", Value = "false" });

            ViewBag.opc_tipo = opc_tipo;


            return View();
        }
        #endregion

        //*** AÑADIR ***
        // POST: Transaccion Real,Detalle del Libro Contable y actualizacion Libro
        #region
        [HttpPost]
        public ActionResult Add(TransRealViewModel model)
        {
            sesion_Usuario = (USUARIO)Session["USUARIO"];

            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                using (var db = new Entities())
                {
                    //Agregando transaccion del presupuesto
                    TRANSACCION_REAL oTraReal = new TRANSACCION_REAL();

                    oTraReal.RUB_ID = model.rub_id;
                    oTraReal.TIPO = model.tipo;
                    oTraReal.FECHA = model.fecha;
                    oTraReal.SUBTOTAL = model.subtotal;
                    oTraReal.IMPUESTO = model.impuesto;
                    oTraReal.TOTAL = model.total;
                    oTraReal.USU_ID = sesion_Usuario.ID_USUARIO;
                    oTraReal.FACTURA_ID = 1;

                    db.TRANSACCION_REAL.Add(oTraReal);
                    db.SaveChanges();

                    //Agregando detalle de la transaccion al presupuesto
                    int id_TransLib = oTraReal.ID_TRANS_REAL;

                    using (var db2 = new Entities())
                    {
                        REG_DET_LIBROS oDetLib = new REG_DET_LIBROS();
                        oDetLib.LIB_ID = model.lib_id;
                        oDetLib.TRA_ID = id_TransLib;


                        db2.REG_DET_LIBROS.Add(oDetLib);
                        db2.SaveChanges();
                    }

                    //Actilizando Valores del presupuesto
                    using (var dbLib = new Entities())
                    {
                        var oLib = dbLib.LIBRO_CONTABLE.Find(model.lib_id);

                        if (!model.tipo) //Si el tipo es falso = gasto 
                        {
                            oLib.TOTAL_GASTOS = oLib.TOTAL_GASTOS + model.total;
                        }
                        else
                        {
                            oLib.TOTAL_INGRESOS = oLib.TOTAL_INGRESOS + model.total;
                        }

                        dbLib.Entry(oLib).State = System.Data.Entity.EntityState.Modified;
                        dbLib.SaveChanges();
                    }
                }
                return Redirect(Url.Content("~/LibroContable/"));
            }
            catch (DbUpdateException e)
            {
                var causa = analizeCaseError(e, "causa");
                var consecuencia = analizeCaseError(e, "consecuencia");
                _logger.Info("Causa: " + causa.ToString() + "\nConsecuencia: " + consecuencia.ToString());

                return Redirect(Url.Action("Add", "TransReal"));
            }
        }
        #endregion

        //*** ANALISIS DE EXCEPCIONES ***
        #region
        public StringBuilder analizeCaseError(DbUpdateException e, string suceso)
        {
            var analisis = new StringBuilder();

            if (suceso.Equals("causa"))
            {
                analisis.AppendLine($"DbUpdateException detalle: {e?.InnerException?.InnerException?.Message}");
            }
            else
            {
                foreach (var eve in e.Entries)
                {
                    analisis.AppendLine($"Entidad: {eve.Entity.GetType().Name} en estado {eve.State} no pudo ser actualizada");
                }
            }

            return analisis;
        }
        #endregion

    }
}