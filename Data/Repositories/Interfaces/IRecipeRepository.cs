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
    }
}





