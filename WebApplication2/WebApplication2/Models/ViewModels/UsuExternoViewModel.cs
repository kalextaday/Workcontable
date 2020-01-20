using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models.ViewModels
{
    public class UsuExternoViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string nombre_usuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string password { get; set; }

        [Display(Name = "Confirmar Contraseña")]
        [Compare("password", ErrorMessage = "Las contraseñas no son iguales")]
        public string confirmPassword { get; set; }

        //[Required]
        [Display(Name = "Rol")]
        public string rol_id { get; set; }

        [Required]
        [Display(Name = "Nombres")]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Apellidos")]
        public string apellido { get; set; }

        [Required]
        [Display(Name = "Cedula")]
        public string cedula { get; set; }

        [Required]
        [Display(Name = "RUC")]
        public string ruc { get; set; }

        [Required]
        [Display(Name = "Direccion")]
        public string direccion { get; set; }

        [Required]
        [Display(Name = "Telefono")]
        public string telefono { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Correo Electronico")]
        public string email { get; set; }
    }
}