using Foodies.Api.Data.Models;
using Foodies.Api.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Foodies.Api.Data.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        /// <summary>
        ///  Context de connexion à la base de données
        /// </summary>
        private readonly FoodiesDBContext _dBContext;

        public RecipeRepository(FoodiesDBContext dBContext)
        {
            _dBContext = dBContext;
        }


        /// <summary>
        /// Cette méthode permet de créer une recettte.
        /// </summary>
        /// <param name="Recipe">The recipe</param>
        /// <returns></returns>
        public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
        {
            var elementAdded = await _dBContext.Recipes.AddAsync(recipe).ConfigureAwait(false);
            await _dBContext.SaveChangesAsync().ConfigureAwait(false);
            return elementAdded.Entity;
        }

        /// <summary>
        /// Cette méthode permet de récupérer les informations d'une recette par son titre.
        /// </summary>
        /// <param name="name">Le titre de la recette.</param>
        /// <returns></returns>
        public async Task<Recipe> GetRecipeByTitleAsync(string title)
        {
            return await _dBContext.Recipes.FirstOrDefaultAsync(recipe => recipe.RecipeTitle == title)
                .ConfigureAwait(false);
        }


        /// <summary>
        /// Cette méthode permet de récupérer les informations d'une recette par son titre.
        /// </summary>
        /// <param name="name">Le titre de la recette.</param>
        /// <returns></returns>
        public async Task<List<Recipe>> GetRecipesByUserNameAsync(string userName)
        {
            var recipesByUserName = await _dBContext.Recipes
           .Where(r => r.RecipeUserName == userName)
           .ToListAsync().ConfigureAwait(false);

            return recipesByUserName;
        }

    }
}
