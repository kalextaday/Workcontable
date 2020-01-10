using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.TableViewModels
{
    public class DetLBTableViewModel
    {
        public long id_reg_det_lib { get; set; }
        public int tra_id { get; set; }
        public int lib_id { get; set; }

        public int id_trans_real { get; set; }
        public int rub_id { get; set; }
        public string rubro_nombre { get; set; }
        public int factura_id { get; set; }
        public bool tipo { get; set; }
        public string tipo_nombre { get; set; }
        public DateTime fecha { get; set; }
        public decimal subtotal { get; set; }
        public decimal impuesto { get; set; }
        public decimal total { get; set; }
    }
}