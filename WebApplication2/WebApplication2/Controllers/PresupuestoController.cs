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
using OfficeOpenXml;

namespace WebApplication2.Controllers
{
    public class PresupuestoController : Controller
    {
        USUARIO sesion_Usuario = null;

        // GET: Presupuesto
        //[AuthorizeUser(new int[] {7})] //dar autorizacion al usuario de este action result
        public ActionResult Index()
        {
            sesion_Usuario = (USUARIO)Session["USUARIO"];

            ViewBag.rol = sesion_Usuario.ROL_ID;

            List<PresuTableViewModel> lst = null;
            using (Entities db = new Entities())
            {
                lst = (from d in db.PRESUPUESTO
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

            return View(lst);
        }


        //*** EXPORTAR ***
        // GET: Tabla
        public void Export()
        {
            List<PresuTableViewModel> lst = null;
            using (Entities db = new Entities())
            {
                lst = (from d in db.PRESUPUESTO
                       select new PresuTableViewModel
                       {
                           id_presupuesto = d.ID_PRESUPUESTO,
                           fecha_inicio = d.FECHA_INICIO,
                           fecha_fin = d.FECHA_FIN,
                           valor_pre = d.VALOR_PRE,
                           total_gastos = d.TOTAL_GASTOS
                       }).ToList();
            }

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "PRESUPUESTO ID";
            ws.Cells["B1"].Value = "FECHA INICIO";
            ws.Cells["C1"].Value = "FECHA FIN";
            ws.Cells["D1"].Value = "VALOR PRESUPUESTO";
            ws.Cells["E1"].Value = "TOTAL GASTOS";

            int rowStart = 2;
            foreach (var item in lst)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.fecha_inicio;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.fecha_inicio;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.fecha_fin;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.valor_pre;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.total_gastos;

                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }


        //*** AÑADIR ***
        // GET: Presupuesto
        [HttpGet]
        public ActionResult Add()
        {
            sesion_Usuario = (USUARIO)Session["USUARIO"];

            ViewBag.rol = sesion_Usuario.ROL_ID;

            return View();
        }


        //*** AÑADIR ***
        // POST: Presupuesto
        [HttpPost]
        public ActionResult Add(PresupuestoViewModel model)
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

                    PRESUPUESTO oPresu = new PRESUPUESTO();

                    oPresu.USU_ID = sesion_Usuario.ID_USUARIO;
                    oPresu.FECHA_INICIO = model.fecha_inicio;
                    oPresu.FECHA_FIN = model.fecha_fin;
                    oPresu.VALOR_PRE = model.valor_pre;
                    oPresu.TOTAL_GASTOS = 0;

                    db.PRESUPUESTO.Add(oPresu);
                    db.SaveChanges();
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entidad de tipo \"{0}\" en estado \"{1}\" tiene los siguientes errores:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Propiedad: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }


            return Redirect(Url.Content("~/Presupuesto/"));
        }


        //*** EDITAR ***
        // GET: Presupuesto
        public ActionResult Edit(int id)
        {

            sesion_Usuario = (USUARIO)Session["USUARIO"];

            ViewBag.rol = sesion_Usuario.ROL_ID;

            EditPresupuestoViewModel model = new EditPresupuestoViewModel();

            using (var db = new Entities())
            {
                var oPresu = db.PRESUPUESTO.Find(id);
                model.id_presupuesto = oPresu.ID_PRESUPUESTO;
                model.fecha_inicio = oPresu.FECHA_INICIO;
                model.fecha_fin = oPresu.FECHA_FIN;
                model.valor_pre = oPresu.VALOR_PRE;
                model.total_gastos = oPresu.TOTAL_GASTOS;
            }

            return View(model);
        }

        //*** EDITAR ***
        // POST: Presupuesto
        [HttpPost]
        public ActionResult Edit(EditPresupuestoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            using (var db = new Entities())
            {
                var oPresu = db.PRESUPUESTO.Find(model.id_presupuesto);

                oPresu.FECHA_INICIO = model.fecha_inicio;
                oPresu.FECHA_FIN = model.fecha_fin;
                oPresu.VALOR_PRE = model.valor_pre;
                oPresu.TOTAL_GASTOS = model.total_gastos;

                db.Entry(oPresu).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }

            return Redirect(Url.Content("~/Presupuesto/"));
        }

        //*** TRANSACCIONES PRESUPUESTO***
        // GET: Detalle Presupuesto

        public ActionResult Record(int id)
        {
            ViewBag.id_presu = id;

            List<DetPreTableViewModel> lst_det_pre = null;
            using (Entities db = new Entities())
            {
                lst_det_pre = (from d in db.REG_DET_PRESUPUESTOS
                       where d.PRE_ID == id
                       select new DetPreTableViewModel
                       {
                           id_reg_det_pre = d.ID_REG_DET_PRE,
                           pre_id = d.PRE_ID,
                           tra_pre_id = d.TRA_PRE_ID
                       }).ToList();
            }

            using (var db = new Entities())
            {
                foreach (var oDetalle in lst_det_pre)
                {
                    var oTransaccion = db.TRANSACCION_PRE.Find(oDetalle.tra_pre_id);

                    oDetalle.rub_id = oTransaccion.RUB_ID;
                    using (var db2 = new Entities())
                    {
                        var oRubro = db2.RUBRO.Find(oDetalle.rub_id);
                        oDetalle.rubro_nombre = oRubro.NOMBRE;
                    }


                    oDetalle.tipo = oTransaccion.TIPO;
                    if (oDetalle.tipo)
                    {
                        oDetalle.tipo_nombre = "Ingreso";
                    }
                    else
                    {
                        oDetalle.tipo_nombre = "Gasto";
                    }
                    oDetalle.fecha = oTransaccion.FECHA;
                    oDetalle.subtotal = oTransaccion.SUBTOTAL;
                    oDetalle.impuesto = oTransaccion.IMPUESTO;
                    oDetalle.total = oTransaccion.TOTAL;
                }
            }

            return View(lst_det_pre);
        }

    }
}