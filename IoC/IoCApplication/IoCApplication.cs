using Foodies.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Foodies.Api.IoC.IoCApplication
{
    public static class IoCApplication
    {
        public static IServiceCollection ConfigureInjectionDependencyRepository(this IServiceCollection services)
        {
            return services;
        }
        public static IServiceCollection ConfigureInjectionDependencyService(this IServiceCollection services)
        {
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
