using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.TableViewModels
{
    public class EstadoResulTableViewModel
    {
        public int id_est_res { get; set; }
        public int lib_contable_id { get; set; }
        public decimal ingresos { get; set; }
        public decimal utilidad_bruta { get; set; }
        public decimal gastos { get; set; }
        public decimal utilidad_antes_imp { get; set; }
        public decimal impuestos { get; set; }
        public decimal utilidad_ejercicio { get; set; }

        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }
        

    }
}