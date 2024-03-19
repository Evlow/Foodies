using Foodies.Api.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Foodies.Api.Data
{
    public class FoodiesDBContext : IdentityDbContext<User>
    {
        // Déclaration des DbSet pour chaque entité dans la base de données
        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Favori> Favoris { get; set; }

        // Constructeur prenant les options de contexte
        public FoodiesDBContext(DbContextOptions<FoodiesDBContext> options) : base(options)
        {
        }

        // Méthode pour configurer les relations entre les entités
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Configuration de la relation entre Recipe et User
            builder.Entity<Recipe>()
                    .HasOne(r => r.User)
                    .WithMany(u => u.Recipes)
                    .HasForeignKey(r => r.UserId);

            base.OnModelCreating(builder); // Appel à la méthode de base pour compléter la configuration du modèle
            SeedRoles(builder); // Appel à la méthode pour initialiser les rôles
        }

        // Méthode pour initialiser les rôles
        private static void SeedRoles(ModelBuilder builder)
        {
            // Ajout de données de rôles par défaut dans la base de données
            builder.Entity<IdentityRole>().HasData
            (
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
            );
        }
    }
}
