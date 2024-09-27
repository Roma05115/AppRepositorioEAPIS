using AppRepositorioEAPIS.Models;
using Microsoft.EntityFrameworkCore;

namespace AppRepositorioEAPIS.Data
{
    public class BDcontexto:DbContext
    {
        public BDcontexto(DbContextOptions opciones) : base(opciones)
        {

        }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Docente> Docentes { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Practica> Practicas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empresa>().ToTable("Empresa");
            modelBuilder.Entity<Docente>().ToTable("Docente");
            modelBuilder.Entity<Estudiante>().ToTable("Estudiante");
            modelBuilder.Entity<Practica>().ToTable("Practica");
			base.OnModelCreating(modelBuilder);
		}
	}
}
