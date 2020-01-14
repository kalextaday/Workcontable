using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication2.Models.ViewModels
{
    public class LBViewModel
    {
        public int id_lib_contable { set; get; }

        public int usu_id { set; get; }

        [Required]
        [Display(Name = "Fecha de inicio")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        public DateTime fecha_inicio { set; get; }

        [Display(Name = "Fecha de Finalizacion")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:yyyy/MM/dd}")]
        public DateTime fecha_fin { set; get; }

        [Required]
        [Display(Name = "Total de Ingresos")]
        public decimal total_ingresos { set; get; }

        [Required]
        [Display(Name = "Total de Gastos")]
        public decimal total_gastos { set; get; }
    }

    public class EditLBViewModel
    {
        public int id_lib_contable { set; get; }

        public int usu_id { set; get; }


        [Required]
        [Display(Name = "Fecha de inicio")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        public DateTime fecha_inicio { set; get; }

        [Display(Name = "Fecha de Finalizacion")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:yyyy/MM/dd}")]
        public DateTime fecha_fin { set; get; }

        [Required]
        [Display(Name = "Total de Ingresos")]
        public decimal total_ingresos { set; get; }

        [Required]
        [Display(Name = "Total de Gastos")]
        public decimal total_gastos { set; get; }

    }
}