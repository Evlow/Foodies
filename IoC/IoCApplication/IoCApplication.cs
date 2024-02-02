using Foodies.Api.Business.Services.Interfaces;
using Foodies.Api.Business.Services;
using Foodies.Api.Data;
using Foodies.Api.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Foodies.Api.Data.Repositories;

namespace Foodies.Api.IoC.IoCApplication
{
    public static class IoCApplication
    {
        public static IServiceCollection ConfigureInjectionDependencyRepository(this IServiceCollection services)
        {

            services.AddScoped<IRecipeRepository, RecipeRepository>();
            return services;
        }
        public static IServiceCollection ConfigureInjectionDependencyService(this IServiceCollection services)
        {

            services.AddScoped<IRecipeService, RecipeService>();

            return services;
        }
        public static IServiceCollection ConfigureDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("BddConnection");

            services.AddDbContext<FoodiesDBContext>(
                options => options.UseMySql(
                    connectionString, ServerVersion.AutoDetect(connectionString),
                      b => b.MigrationsAssembly("Foodies.Api"))
                      .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
                    );

            return services;
        }
    }
}
