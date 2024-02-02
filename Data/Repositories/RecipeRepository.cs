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
           .Where(r => r.UserName == userName)
           .ToListAsync().ConfigureAwait(false);

            return recipesByUserName;
        }


        /// <summary>
        /// Cette méthode permet de mettre une unité de mesure .
        /// </summary>
        /// <param name="unity">The unite.</param>
        /// <returns></returns>
        public async Task<Recipe> UpdateRecipeAsync(Recipe recipe)
        {
            var elementUpdated = _dBContext.Recipes.Update(recipe);

            await _dBContext.SaveChangesAsync().ConfigureAwait(false);

            return elementUpdated.Entity;
        }
        /// <summary>
        /// Cette méthode permet de supprimer une unité de mesure.
        /// </summary>
        /// <param name="unity">The unite.</param>
        /// <returns></returns>
        public async Task<Recipe> DeleteRecipeAsync(Recipe recipe)
        {
            var elementDeleted = _dBContext.Recipes.Remove(recipe);
            await _dBContext.SaveChangesAsync().ConfigureAwait(false);
            return elementDeleted.Entity;
        }

        public async Task<List<Recipe>> GetRecipesByCategoryIdAsync(int categoryId)
        {
            var recipesByCategoryId = await _dBContext.Recipes
           .Where(r => r.CategoryId == categoryId)
           .ToListAsync().ConfigureAwait(false);

            return recipesByCategoryId;
        }


        /// <summary>
        /// Cette méthode permet de récupérer la liste de toutes les unités de mesure.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Recipe>> GetRecipesAsync()
        {
            return await _dBContext.Recipes.ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Cette méthode permet de récupérer les informations d'une unité de mesure par identifiant.
        /// </summary>
        /// <param name="id">L'identifiant.</param>
        /// <returns></returns>
        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {

            return await _dBContext.Recipes.FirstOrDefaultAsync(recipe => recipe.RecipeId == id);

        }

    }
}
