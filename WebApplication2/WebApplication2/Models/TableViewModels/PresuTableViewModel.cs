using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.TableViewModels
{
    public class PresuTableViewModel
    {
        public int id_presupuesto { set; get; }
        public DateTime fecha_inicio { set; get; }
        public DateTime fecha_fin { set; get; }
        public decimal valor_pre { set; get; }
        public decimal total_gastos { set; get; }
    }
}