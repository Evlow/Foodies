using Foodies.Api.Data;
using Foodies.Api.Data.Repositories.Interfaces;
using Foodies.Api.Data.Repositories;
using Foodies.Api.IoC.IoCApplication;
using Microsoft.EntityFrameworkCore;
using Foodies.Api.Business.Services.Interfaces;
using Foodies.Api.Business.Services;

namespace Foodies.Api.IoC.IoCTest
{
    public static class IoCTests
    {
        /// <summary>
        /// Configuration de l'injection des repository du Web API RestFul
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection ConfigureInjectionDependencyRepositoryTest(this IServiceCollection services)
        {
            services.AddScoped<IRecipeRepository, RecipeRepository>();

            return services;

        }


        /// <summary>
        /// Configure l'injection des services
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection ConfigureInjectionDependencyServiceTest(this IServiceCollection services)
        {
            services.AddScoped<IRecipeService, RecipeService>();

            return services;
        }


        /// <summary>
        /// Configuration de la connexion de la base de données en mémoire pour l'environnement de test
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection ConfigureDBContextTest(this IServiceCollection services)
        {
            services.AddDbContext<FoodiesDBContext>(options =>
                options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()));

            return services;
        }
    }
}
