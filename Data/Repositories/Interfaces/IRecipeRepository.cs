using Foodies.Api.Data.Models;

namespace Foodies.Api.Data.Repositories.Interfaces
{
    public interface IRecipeRepository
    {
        /// <summary>s
        /// Cette méthode permet de créer une recette.
        /// </summary>
        /// <param name="Recipe">The recipe.</param>
        /// <returns></returns>
        Task<Recipe> CreateRecipeAsync(Recipe recipe);

        /// <summary>
        /// Cette méthode permet de récupérer les informations d'une recette par son titre
        /// </summary>
        /// <param name="name">Le titre de la recette.</param>
        /// <returns></returns>
        Task<Recipe> GetRecipeByTitleAsync(string title);

        Task<List<Recipe>> GetRecipesByUserNameAsync(string userName);

        /// <summary>
        /// Cette méthode permet de récupérer la liste de toutes les mesures.
        /// </summary>
        /// <returns></returns>
        Task<List<Recipe>> GetRecipesAsync();

        /// <summary>
        /// Cette méthode permet de récupérer les informations d'une mesure par identifiant.
        /// </summary>
        /// <param name="id">L'identifiant.</param>
        /// <returns></returns>
        Task<Recipe> GetRecipeByIdAsync(int id);

        /// <summary>
        /// Cette méthode permet de mettre à jour une unité de mesure .
        /// </summary>
        /// <param name="unity">The unite.</param>
        /// <returns></returns>
        Task<Recipe> UpdateRecipeAsync(Recipe recipe);


        /// <summary>
        /// Cette méthode permet de supprimer une unité de mesure.
        /// </summary>
        /// <param name="unity">The unite.</param>
        /// <returns></returns>
        Task<Recipe> DeleteRecipeAsync(Recipe recipe);

        Task<List<Recipe>> GetRecipesByCategoryIdAsync(int categoryId);
    }
}





