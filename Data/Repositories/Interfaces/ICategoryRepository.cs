using Foodies.Api.Data.Models;

namespace Foodies.Api.Data.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// Cette méthode permet de récupérer la liste des catégories
        /// </summary>
        /// <returns></returns>
        Task<List<Category>> GetCategoriesAsync();

        /// <summary>
        /// Cette méthode permet de récupérer les informations d'une catégorie par identifiant.
        /// </summary>
        /// <param name="id">L'identifiant.</param>
        /// <returns></returns>
        Task<Category> GetCategoryByIdAsync(int id);

        /// <summary>
        /// Cette méthode permet de récupérer les informations d'une catégorie par son nom.
        /// </summary>
        /// <param name="name">le nom de la catégorie de mesure.</param>
        /// <returns></returns>
        Task<Category> GetCategoryByNameAsync(string name);

        /// <summary>
        /// Cette méthode permet de créer une catégorie.
        /// </summary>
        /// <param name="Category">The category.</param>
        /// <returns></returns>
        Task<Category> CreateCategoryAsync(Category category);

        /// <summary>
        /// Cette méthode permet de mettre à jour une catégorie.
        /// </summary>
        /// <param name="Category">The category.</param>
        /// <returns></returns>
        Task<Category> UpdateCategoryAsync(Category category);


        /// <summary>
        /// Cette méthode permet de supprimer une catégorie.
        /// </summary>
        /// <param name="Category">The category.</param>
        /// <returns></returns>
        Task<Category> DeleteCategoryAsync(Category category);
    }
}
