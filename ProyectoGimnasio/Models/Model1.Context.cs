﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoGimnasio.Models
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
    
        public virtual DbSet<AsistenciaAClase> AsistenciaAClase { get; set; }
        public virtual DbSet<Clase> Clase { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Opinion> Opinion { get; set; }
        public virtual DbSet<Pagina> Pagina { get; set; }
        public virtual DbSet<PlanMembresia> PlanMembresia { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<RolPagina> RolPagina { get; set; }
        public virtual DbSet<Sexo> Sexo { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Turno> Turno { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
    }
}