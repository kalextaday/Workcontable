﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<LIBRO_CONTABLE> LIBRO_CONTABLE { get; set; }
        public virtual DbSet<MODULO> MODULO { get; set; }
        public virtual DbSet<OPERACION> OPERACION { get; set; }
        public virtual DbSet<PRESUPUESTO> PRESUPUESTO { get; set; }
        public virtual DbSet<REG_DET_LIBROS> REG_DET_LIBROS { get; set; }
        public virtual DbSet<REG_DET_PRESUPUESTOS> REG_DET_PRESUPUESTOS { get; set; }
        public virtual DbSet<ROL> ROL { get; set; }
        public virtual DbSet<ROL_OPERACION> ROL_OPERACION { get; set; }
        public virtual DbSet<RUBRO> RUBRO { get; set; }
        public virtual DbSet<TRANSACCION_PRE> TRANSACCION_PRE { get; set; }
        public virtual DbSet<TRANSACCION_REAL> TRANSACCION_REAL { get; set; }
        public virtual DbSet<USUARIO> USUARIO { get; set; }
        public virtual DbSet<EMPRESA> EMPRESA { get; set; }
        public virtual DbSet<IDIOMA> IDIOMA { get; set; }
        public virtual DbSet<ESTADO_RESULTADO> ESTADO_RESULTADO { get; set; }
        public virtual DbSet<FACTURA> FACTURA { get; set; }
    }
}
