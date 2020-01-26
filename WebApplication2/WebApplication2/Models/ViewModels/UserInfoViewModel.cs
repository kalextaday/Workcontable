using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModels
{
    public class UserInfoViewModel
    {
        public int id_usuario { get; set; }
        public byte rol_id { get; set; }
        public string rol_nombre { get; set; }
        public string nombre_usuario { get; set; }
        public string password { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string cedula { get; set; }
        public string ruc { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public List<IdiomaViewModel> lstIdiomas { get; set; }
    }
}