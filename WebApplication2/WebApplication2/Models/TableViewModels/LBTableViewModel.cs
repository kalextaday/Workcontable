using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.TableViewModels
{
    public class LBTableViewModel
    {
        public int id_lib_contable { get; set; }
        public int usu_id { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }
        public decimal total_ingresos { get; set; }
        public decimal total_gastos { get; set; }
    }
}