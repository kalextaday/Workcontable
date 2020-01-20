using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.TableViewModels
{
    public class ComparativoTableViewModel
    {
        //FACTOR COMUN
        public int usu_id { get; set; }
        public decimal varianza { get; set; }

        //PRESUPUESTO
        public int id_presupuesto { get; set; }
        public DateTime fecha_inicio_pre { get; set; }
        public DateTime fecha_fin_pre { get; set; }
        public decimal valor_pre { get; set; }
        public decimal total_gastos_pre { get; set; }

        //LIBRO CONTABLE
        public int id_lib_contable { get; set; }
        public DateTime fecha_inicio_lib { get; set; }
        public DateTime fecha_fin_lib { get; set; }
        public decimal total_ingresos { get; set; }
        public decimal total_gastos_lib { get; set; }
    }
}