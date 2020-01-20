using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModels
{
    public class TransRealViewModel
    {
        [Required]
        [Display(Name = "Rubro")]
        public int rub_id { get; set; }

        [Required]
        [Display(Name = "ID Libro Contable")]
        public int lib_id { get; set; }

        [Required]
        [Display(Name = "ID Usuario")]
        public int usu_id { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public bool tipo { get; set; }

        [Required]
        [Display(Name = "Fecha")]
        public DateTime fecha { get; set; }

        [Required]
        [Display(Name = "Subtotal")]
        public decimal subtotal { get; set; }

        [Required]
        [Display(Name = "Impuesto")]
        public decimal impuesto { get; set; }

        [Required]
        [Display(Name = "Total")]
        public decimal total { get; set; }

        [Required]
        [Display(Name = "Factura")]
        public int factura_id { get; set; }
    }

    public class FacturaViewModel{

        public int id_factura { get; set; }
        public string numero { get; set; }
        public byte[] foto { get; set; }

        public HttpPostedFileBase ImageFile { get; set; } //es la ruta de la imagen


    }
}