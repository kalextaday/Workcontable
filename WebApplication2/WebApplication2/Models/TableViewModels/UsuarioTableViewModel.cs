using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.TableViewModels
{
    public class UsuarioTableViewModel
    {
        public int id_usuario { get; set; }
        public int rol_id { get; set; }
        public int per_id { get; set; }
        public string nombre_usuario { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string cedula { get; set; }
        public string ruc { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
    }
}