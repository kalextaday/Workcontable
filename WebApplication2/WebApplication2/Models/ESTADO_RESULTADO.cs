//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ESTADO_RESULTADO
    {
        public int ID_EST_RES { get; set; }
        public int LIB_CONTABLE_ID { get; set; }
        public decimal UTILIDAD_BRUTA { get; set; }
        public decimal UTILIDAD_ANTES_IMP { get; set; }
        public decimal UTILIDAD_EJERCICIO { get; set; }
        public System.DateTime FECHA_INICIO { get; set; }
        public System.DateTime FECHA_FIN { get; set; }
    
        public virtual LIBRO_CONTABLE LIBRO_CONTABLE { get; set; }
    }
}
