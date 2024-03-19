using Foodies.Api.Business.Services.Interfaces;
using Foodies.Api.Business.Services;
using Foodies.Api.Data;
using Foodies.Api.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Foodies.Api.Data.Repositories;
using Foodies.Api.Business.Services.interfaces;
using Foodies.Api.Commun.ImageService;

namespace Foodies.Api.IoC.IoCApplication
{
    public static class IoCApplication
    {
        // Méthode pour configurer les dépendances des repositories
        public static IServiceCollection ConfigureInjectionDependencyRepository(
            this IServiceCollection services
        )
        {
            // Configuration des services pour les repositories
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        // Méthode pour configurer les dépendances des services
        public static IServiceCollection ConfigureInjectionDependencyService(
            this IServiceCollection services
        )
        {
            // Configuration des services
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ImageService>();

            return services;
        }

        // Méthode pour configurer le contexte de la base de données
        public static IServiceCollection ConfigureDBContext(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var connectionString = configuration.GetConnectionString("BddConnection");

            // Configuration du contexte de la base de données
            services.AddDbContext<FoodiesDBContext>(
                options =>
                    options
                        .UseMySql(
                            connectionString,
                            ServerVersion.AutoDetect(connectionString),
                            b => b.MigrationsAssembly("Foodies.Api")
                        )
                        .LogTo(Console.WriteLine, LogLevel.Information)
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors()
            );

            return services;
        }
    }

}
