using System.Diagnostics.CodeAnalysis;
using Isen.Dotnet.Library.Model;
using Microsoft.EntityFrameworkCore;

namespace Isen.Dotnet.Library.Context
{    
    public class ApplicationDbContext : DbContext
    {        
        // Listes des classes modèle / tables
        public DbSet<Personne> PersonneCollection { get; set; }
        public DbSet<Role> RoleCollection { get; set; }
        public DbSet<Service> ServiceCollection { get; set; }


        public ApplicationDbContext(
            [NotNullAttribute] DbContextOptions options) : 
            base(options) {  }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tables et relations
            modelBuilder
                .Entity<Personne>()
                .ToTable(nameof(Personne))
                .HasOne(pe => pe.TypeRole)
                .WithMany()
                .HasForeignKey(pe => pe.TypeRoleId);
          
            modelBuilder.Entity<Personne>()
                .HasOne(pe => pe.TypeService)
                .WithMany()
                .HasForeignKey(pe => pe.TypeServiceId);
            
            modelBuilder.Entity<Personne>()
                .HasKey(p => p.Id);

            modelBuilder
                .Entity<Role>()
                .ToTable(nameof(Role))
                .HasKey(r => r.Id);

            modelBuilder
                .Entity<Service>()
                .ToTable(nameof(Service))
                .HasKey(s => s.Id);
            
        }

    }
}