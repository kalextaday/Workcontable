using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModels
{
    public class RolOperacionViewModel
    {
        public int id_rol_operacion { get; set; }
        public int usuario_id { get; set; }
        public int operacion_id { get; set; }
        public bool estado { get; set; }
    }
}