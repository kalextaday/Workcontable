using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models.ViewModels
{
    public class PresupuestoViewModel
    {

        public int id_presupuesto { set; get; }

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
        [Display(Name = "Valor de presupuesto")]
        public decimal valor_pre { set; get; }

        [Required]
        [Display(Name = "Total de Gastos")]
        public decimal total_gastos { set; get; }
    }

    public class EditPresupuestoViewModel
    {
        public int id_presupuesto { set; get; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de inicio")]
        public DateTime fecha_inicio { set; get; }


        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de Finalizacion")]
        public DateTime fecha_fin { set; get; }

        [Required]
        [Display(Name = "Valor de presupuesto")]
        public decimal valor_pre { set; get; }

        [Required]
        [Display(Name = "Total de Gastos")]
        public decimal total_gastos { set; get; }
        
    }
}