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
    
    public partial class PRESUPUESTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRESUPUESTO()
        {
            this.REG_DET_PRESUPUESTOS = new HashSet<REG_DET_PRESUPUESTOS>();
        }
    
        public int ID_PRESUPUESTO { get; set; }
        public int USU_ID { get; set; }
        public System.DateTime FECHA_INICIO { get; set; }
        public System.DateTime FECHA_FIN { get; set; }
        public decimal VALOR_PRE { get; set; }
        public decimal TOTAL_GASTOS { get; set; }
    
        public virtual USUARIO USUARIO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REG_DET_PRESUPUESTOS> REG_DET_PRESUPUESTOS { get; set; }
    }
}
