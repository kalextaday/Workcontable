using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.TableViewModels
{
    public class TransPreTableViewModel
    {
        public int id_trans_pre { get; set; }
        public int rub_id { get; set; }
        public bool tipo { get; set; }
        public DateTime fecha { get; set; }
        public decimal subtotal { get; set; }
        public decimal impuesto { get; set; }
        public decimal total { get; set; }
        public int usu_id { get; set; }
    }
}