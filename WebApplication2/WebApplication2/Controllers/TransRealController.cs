﻿using System;
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
    public class TransRealController : Controller
    {

        USUARIO sesion_Usuario = null;
        int libContable_id;

        // GET: Add TransPresu
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


                    int id_TransPre = oTraReal.ID_TRANS_REAL;

                    using (var db2 = new Entities())
                    {
                        REG_DET_LIBROS oDetLib = new REG_DET_LIBROS();
                        oDetLib.LIB_ID = model.lib_id;
                        oDetLib.TRA_ID = id_TransPre;


                        db2.REG_DET_LIBROS.Add(oDetLib);
                        db2.SaveChanges();
                    }

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


    }
}