using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models.TableViewModels
{
    public class PermisoTableViewModel
    {
        public int id_rol_operacion { get; set; }

        [Display(Name = "Usuario: ")]
        public int usuario_id { get; set; }
        public int operacion_id { get; set; }
        public bool estado { get; set; }

        public string rol_nombre { get; set; }
        public string operacion_nombre { get; set; }
        public string modulo_nombre { get; set; }
    }

    public class RolOperaTableViewModel
    {
        [Display(Name = "Usuario: ")]
        public int usuario_id { get; set; }

        public int id_rol_operacion { get; set; }
        public string rol_nombre { get; set; }
        public string operacion_nombre { get; set; }
        public string modulo_nombre { get; set; }
    }
}