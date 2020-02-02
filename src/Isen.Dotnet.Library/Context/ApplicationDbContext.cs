using System.Diagnostics.CodeAnalysis;
using Isen.Dotnet.Library.Model;
using Microsoft.EntityFrameworkCore;

namespace Isen.Dotnet.Library.Context
{    
    public class ApplicationDbContext : DbContext
    {        
        // Listes des classes modèle / tables
        public DbSet<Personne> PersonneCollection { get; set; }
        public DbSet<Service> ServiceCollection { get; set; }
        public DbSet<Role> RoleCollection { get; set; }
        public ApplicationDbContext(
            [NotNullAttribute] DbContextOptions options) : 
            base(options) {  }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tables et relations
            modelBuilder
                .Entity<Personne>() //association avec la classe Personne
                .ToTable(nameof(Personne))  //assocation avec la table Personne
                .HasOne(p => p.TypeRole)    //représente la relation avec Personne.TypeRole
                .WithMany()
                .HasForeignKey(p => p.TypeRoleId);    //clé étrangère
            
            modelBuilder
                .Entity<Personne>()
                .HasOne(p => p.TypeService)
                .WithMany()
                .HasForeignKey(p => p.TypeServiceId);

            modelBuilder
                .Entity<Personne>()
                .HasKey(p => p.Id);
            
            modelBuilder
                .Entity<Service>()
                .ToTable(nameof(Service))
                .HasKey(s => s.Id);
            
            modelBuilder
                .Entity<Role>()
                .ToTable(nameof(Role))
                .HasKey(r => r.Id);
                
        }

    }
}