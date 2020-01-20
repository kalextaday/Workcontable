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
    public class TransPresuController : Controller
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        USUARIO sesion_Usuario = null;


        //*** AÑADIR ***
        // GET: TransPresu
        #region
        public ActionResult Add(int id)
        {
            ViewBag.id_presu = id;

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
            opc_tipo.Add(new SelectListItem { Text = "Ingreso", Value="true" });
            opc_tipo.Add(new SelectListItem { Text = "Gasto", Value = "false" });

            ViewBag.opc_tipo = opc_tipo;


            return View();
        }
        #endregion

        //*** AÑADIR ***
        // POST: Transaccion Presupuesto,Detalle del presupuesto y actualizacion Presupuesto
        #region
        [HttpPost]
        public ActionResult Add(TransPreViewModel model)
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
                    TRANSACCION_PRE oTraPresu = new TRANSACCION_PRE();

                    oTraPresu.RUB_ID = model.rub_id;
                    oTraPresu.TIPO = model.tipo;
                    oTraPresu.FECHA = model.fecha;
                    oTraPresu.SUBTOTAL = model.subtotal;
                    oTraPresu.IMPUESTO = model.impuesto;
                    oTraPresu.TOTAL = model.total;
                    oTraPresu.USU_ID = sesion_Usuario.ID_USUARIO;

                    db.TRANSACCION_PRE.Add(oTraPresu);
                    db.SaveChanges();

                    //Agregando detalle de la transaccion al presupuesto
                    int id_TransPre = oTraPresu.ID_TRANS_PRE;

                    using (var db2 = new Entities())
                    {
                        REG_DET_PRESUPUESTOS oDetPre = new REG_DET_PRESUPUESTOS();
                        oDetPre.PRE_ID = model.pre_id;
                        oDetPre.TRA_PRE_ID = id_TransPre;


                        db2.REG_DET_PRESUPUESTOS.Add(oDetPre);
                        db2.SaveChanges();
                    }

                    //Actilizando Valores del presupuesto
                    using (var dbPre = new Entities())
                    {
                        var oPresu = dbPre.PRESUPUESTO.Find(model.pre_id);

                        if (!model.tipo) //Si el tipo es falso = gasto 
                        {
                            oPresu.TOTAL_GASTOS = oPresu.TOTAL_GASTOS + model.total;
                        }
                        else
                        {
                            oPresu.VALOR_PRE = oPresu.VALOR_PRE + model.total;
                        }

                        dbPre.Entry(oPresu).State = System.Data.Entity.EntityState.Modified;
                        dbPre.SaveChanges();
                    }

                }

                return Redirect(Url.Content("~/Presupuesto/"));
            }
            catch (DbUpdateException e)
            {
                var causa = analizeCaseError(e, "causa");
                var consecuencia = analizeCaseError(e, "consecuencia");
                _logger.Info("Causa: " + causa.ToString() + "\nConsecuencia: " + consecuencia.ToString());

                return Redirect(Url.Action("Add", "TransPresu"));
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