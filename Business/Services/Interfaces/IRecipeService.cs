using Foodies.Api.Business.DTOs;

namespace Foodies.Api.Business.Services.Interfaces
{
    public interface IRecipeService
    {
        /// <summary>
        /// Cette méthode permet de récupérer la liste des recettes.
        /// </summary>
        /// <returns></returns>
        Task<List<RecipeDTO>> GetRecipesAsync();

        /// <summary>
        /// Cette méthode permet de récupérer un id d'une recette.
        /// </summary>
        /// <returns></returns>
        Task<RecipeDTO> GetRecipeIdAsync(int recipeId);

        /// <summary>
        /// Cette méthode permet de créer une recette de mesure.
        /// </summary>
        /// <param name = "Recipe" > L'recette à créer.</param>
        /// <returns></returns>
        /// <exception cref = "System.Exception" > Il existe déjà une recette avec ce titre !!</exception>
        Task<RecipeDTO> CreateRecipeAsync(RecipeDTO recipe);

        /// <summary>
        /// Cette méthode permet de mettre à jour une recette.
        /// </summary>
        /// <param name = "RecipeId" > l'identifiant de recette</param>
        /// <param name = "Recipe" > la recette modifiée</param>
        /// <returns></returns>
        /// <exception cref = "System.Exception" >
        /// Il existe déjà une recette avec ce titre!
        /// or
        /// Il n'existe aucune recette avec cet identifiant : {idRecipe}
        /// </exception>
        Task<RecipeDTO> UpdateRecipeAsync(int recipeId, RecipeDTO recipe);

        /// <summary>
        /// Cette méthode permet de supprimer une recette.
        /// </summary>
        /// <param name = "RecipeId" > L'identifiant de la recette à supprimer.</param>
        /// <returns></returns>
        /// <exception cref = "System.Exception" > Il n'existe aucune recette de mesure avec cet identifiant : {idRecipe}</exception>
        Task<RecipeDTO> DeleteRecipeAsync(int recipeId);
        Task<List<RecipeDTO>> GetRecipesByCategoryIdAsync(int categoryId);
        Task<List<RecipeDTO>> GetRecipesByUserNameAsync(string userName);


    }
}
