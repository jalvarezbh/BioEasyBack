using BioEasy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BioEasy.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Paciente> Pacientes { get; set; }
        public virtual DbSet<HistoricoPaciente> HistoricoPacientes { get; set; }
        public virtual DbSet<AnaliseLaboratorial> AnalisesLaboratoriais { get; set; }
        public virtual DbSet<Configuracao> Configuracoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empresa>().ToTable("Empresa");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Paciente>().ToTable("Paciente");
            modelBuilder.Entity<HistoricoPaciente>().ToTable("HistoricoPaciente");
            modelBuilder.Entity<Configuracao>().ToTable("Configuracao");
        }
    }
}
