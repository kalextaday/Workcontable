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
    
    public partial class USUARIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USUARIO()
        {
            this.LIBRO_CONTABLE = new HashSet<LIBRO_CONTABLE>();
            this.PRESUPUESTO = new HashSet<PRESUPUESTO>();
            this.ROL_OPERACION = new HashSet<ROL_OPERACION>();
            this.TRANSACCION_PRE = new HashSet<TRANSACCION_PRE>();
            this.TRANSACCION_REAL = new HashSet<TRANSACCION_REAL>();
            this.IDIOMA = new HashSet<IDIOMA>();
        }
    
        public int ID_USUARIO { get; set; }
        public byte ROL_ID { get; set; }
        public string NOMBRE_USUARIO { get; set; }
        public string PASSWORD { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO { get; set; }
        public string CEDULA { get; set; }
        public string RUC { get; set; }
        public string DIRECCION { get; set; }
        public string TELEFONO { get; set; }
        public string EMAIL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LIBRO_CONTABLE> LIBRO_CONTABLE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESUPUESTO> PRESUPUESTO { get; set; }
        public virtual ROL ROL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ROL_OPERACION> ROL_OPERACION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRANSACCION_PRE> TRANSACCION_PRE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRANSACCION_REAL> TRANSACCION_REAL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IDIOMA> IDIOMA { get; set; }
    }
}
