using Foodies.Api.Business.Services.Interfaces;
using Foodies.Api.Business.Services;
using Foodies.Api.Data;
using Foodies.Api.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Foodies.Api.Data.Repositories;
using Foodies.Api.Business.Services.interfaces;

namespace Foodies.Api.IoC.IoCApplication
{
    public static class IoCApplication
    {
        public static IServiceCollection ConfigureInjectionDependencyRepository(this IServiceCollection services)
        {

            //services.AddScoped<IUnityRepository, UnityRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<ICommentRepository, CommentRepository>();
            //services.AddScoped<IFavoriRepository, FavoriRepository>();
            //services.AddScoped<IIngredientRepository, IngredientRepository>();
            //services.AddScoped<IPreparationRepository, PreparationRepository>();
            //services.AddScoped<IRecipeIngredientRepository, RecipeIngredientRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
        public static IServiceCollection ConfigureInjectionDependencyService(this IServiceCollection services)
        {


            //services.AddScoped<IUnityService, UnityService>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<ICategoryService, CategoryService>();
            //services.AddScoped<ICommentService, CommentService>();
            //services.AddScoped<IFavoriService, FavoriService>();
            //services.AddScoped<IIngredientService, IngredientService>();
            //services.AddScoped<IPreparationService, PreparationService>();
            //services.AddScoped<IRecipeIngredientService, RecipeIngredientService>();
            services.AddScoped<IUserService, UserService>();
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
