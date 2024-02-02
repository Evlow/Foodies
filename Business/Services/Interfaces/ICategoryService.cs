using Foodies.Api.Business.DTOs;

namespace Foodies.Api.Business.Services.interfaces
{
    public interface ICategoryService
    {
        /// <summary>
        /// Cette méthode permet de récupérer les listes des unités de mesure.
        /// </summary>
        /// <returns></returns>
        Task<List<CategoryDTO>> GetCategoriesAsync();

        /// <summary>
        /// Cette méthode permet de récupérer un id d'une unité de mesure.
        /// </summary>
        /// <returns></returns>
        Task<CategoryDTO> GetCategoryIdAsync(int categoryId);

        /// <summary>
        /// Cette méthode permet de créer une unité de mesure.
        /// </summary>
        /// <param name="unity">L'unité à créer.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Il existe déjà une unité de mesure du même nom !!</exception>
        Task<CategoryDTO> CreateCategoryAsync(CategoryDTO category);

        /// <summary>
        /// Cette méthode permet de mettre à jour une unité de mesure .
        /// </summary>
        /// <param name="unityId">l'identifiant de unité</param>
        /// <param name="unity">l'unité modifié</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// Il existe déjà une unité de mesure du même nom !!
        /// or
        /// Il n'existe aucune unité de mesure avec cet identifiant : {idUnite}
        /// </exception>
        Task<CategoryDTO> UpdateCategoryAsync(int categoryId, CategoryDTO category);

        /// <summary>
        /// Cette méthode permet de supprimer une unité de mesure.
        /// </summary>
        /// <param name="UnityId">L'identifiant de l'unité à supprimer.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Il n'existe aucune unité de mesure avec cet identifiant : {idUnite}</exception>
        Task<CategoryDTO> DeleteCategoryAsync(int categoryId);
    }
}

