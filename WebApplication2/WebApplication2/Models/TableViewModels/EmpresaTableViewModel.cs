using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.TableViewModels
{
    public class EmpresaTableViewModel
    {
        public int id_empresa { get; set; }
        public string nombre_comercial { get; set; }
        public string ruc { get; set; }
        public string nombre_responsable { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string actividad_economica { get; set; }
        public string autorizacion_sri { get; set; }
    }
}