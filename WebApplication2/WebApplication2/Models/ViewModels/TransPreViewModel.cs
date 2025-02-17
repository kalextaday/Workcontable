﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModels
{
    public class TransPreViewModel
    {

        [Required]
        [Display(Name = "Rubro")]
        public int rub_id { get; set; }

        [Required]
        [Display(Name = "ID Presupuesto")]
        public int pre_id { get; set; }

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
    }
}