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

namespace WebApplication2.Controllers
{
    public class LibroContableController : Controller
    {
        USUARIO sesion_Usuario = null;

        // GET: LibroContable
        public ActionResult Index()
        {
            sesion_Usuario = (USUARIO)Session["USUARIO"];

            ViewBag.rol = sesion_Usuario.ROL_ID;

            List<LBTableViewModel> lst = null;
            using (Entities db = new Entities())
            {
                lst = (from d in db.LIBRO_CONTABLE
                       where d.USU_ID == sesion_Usuario.ID_USUARIO
                       select new LBTableViewModel
                       {
                           id_lib_contable = d.ID_LIB_CONTABLE,
                           usu_id=d.USU_ID,
                           fecha_inicio = d.FECHA_INICIO,
                           fecha_fin = d.FECHA_FIN,
                           total_ingresos = d.TOTAL_INGRESOS,
                           total_gastos = d.TOTAL_GASTOS
                       }).ToList();
            }

            return View(lst);
        }


        //*** AÑADIR ***
        // GET: LibroContable
        [HttpGet]
        public ActionResult Add()
        {
            sesion_Usuario = (USUARIO)Session["USUARIO"];

            ViewBag.rol = sesion_Usuario.ROL_ID;

            return View();
        }


        //*** AÑADIR ***
        // POST: LibroContable
        [HttpPost]
        public ActionResult Add(LBViewModel model)
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

                    LIBRO_CONTABLE oLB = new LIBRO_CONTABLE();

                    oLB.USU_ID = sesion_Usuario.ID_USUARIO;
                    oLB.FECHA_INICIO = model.fecha_inicio;
                    oLB.FECHA_FIN = model.fecha_fin;
                    oLB.TOTAL_INGRESOS = model.total_ingresos;
                    oLB.TOTAL_GASTOS = model.total_gastos;

                    db.LIBRO_CONTABLE.Add(oLB);
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


            return Redirect(Url.Content("~/LibroContable/"));
        }



        //*** EDITAR ***
        // GET: LibroContable
        public ActionResult Edit(int id)
        {

            sesion_Usuario = (USUARIO)Session["USUARIO"];

            ViewBag.rol = sesion_Usuario.ROL_ID;

            EditLBViewModel model = new EditLBViewModel();

            using (var db = new Entities())
            {
                var oLB = db.LIBRO_CONTABLE.Find(id);
                model.id_lib_contable = oLB.ID_LIB_CONTABLE;
                model.fecha_inicio = oLB.FECHA_INICIO;
                model.fecha_fin = oLB.FECHA_FIN;
                model.total_ingresos = oLB.TOTAL_INGRESOS;
                model.total_gastos = oLB.TOTAL_GASTOS;
            }

            return View(model);
        }

        //*** EDITAR ***
        // POST: LibroContable
        [HttpPost]
        public ActionResult Edit(EditLBViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            using (var db = new Entities())
            {
                var oLB = db.LIBRO_CONTABLE.Find(model.id_lib_contable);

                oLB.FECHA_INICIO = model.fecha_inicio;
                oLB.FECHA_FIN = model.fecha_fin;
                oLB.TOTAL_INGRESOS = model.total_ingresos;
                oLB.TOTAL_GASTOS = model.total_gastos;

                db.Entry(oLB).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }

            return Redirect(Url.Content("~/LibroContable/"));
        }

        //*** TRANSACCIONES LIBROCONTABLE***
        // GET: Detalle LibroContable

        public ActionResult Record(int id)
        {
            ViewBag.id_lb = id;

            List<DetLBTableViewModel> lst_det_lb = null;
            using (Entities db = new Entities())
            {
                lst_det_lb = (from d in db.REG_DET_LIBROS
                               where d.LIB_ID == id
                               select new DetLBTableViewModel
                               {
                                   id_reg_det_lib = d.ID_REG_DET_LIB,
                                   lib_id = d.LIB_ID,
                                   tra_id = d.TRA_ID
                               }).ToList();
            }

            using (var db = new Entities())
            {
                foreach (var oDetalle in lst_det_lb)
                {
                    var oTransaccion = db.TRANSACCION_REAL.Find(oDetalle.tra_id);

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
                    oDetalle.factura_id = oTransaccion.FACTURA_ID;
                }
            }

            return View(lst_det_lb);
        }
    }
}