using Foodies.Api.Data.Models;
using Foodies.Api.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Foodies.Api.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        /// <summary>
        ///  Context de connexion à la base de données
        /// </summary>
        private readonly FoodiesDBContext _dBContext;

        public CategoryRepository(FoodiesDBContext dBContext)
        {
            _dBContext = dBContext;
        }



        /// <summary>
        /// Cette méthode permet de récupérer la liste de toutes les unités de mesure.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _dBContext.Categories.ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Cette méthode permet de récupérer les informations d'une unité de mesure par identifiant.
        /// </summary>
        /// <param name="id">L'identifiant.</param>
        /// <returns></returns>
        public async Task<Category> GetCategoryByIdAsync(int id)
        {

            return await _dBContext.Categories.FirstOrDefaultAsync(category => category.CategoryId == id);

        }

        /// <summary>
        /// Cette méthode permet de récupérer les informations d'une unité de mesure par son nom.
        /// </summary>
        /// <param name="name">le nom de l'unité.</param>
        /// <returns></returns>
        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            return await _dBContext.Categories.FirstOrDefaultAsync(category => category.CategoryName == name)
                .ConfigureAwait(false);
        }


        /// <summary>
        /// Cette méthode permet de créer une unité de mesure.
        /// </summary>
        /// <param name="category">The unite.</param>
        /// <returns></returns>
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            var elementAdded = await _dBContext.Categories.AddAsync(category).ConfigureAwait(false);
            await _dBContext.SaveChangesAsync().ConfigureAwait(false);
            return elementAdded.Entity;
        }


        /// <summary>
        /// Cette méthode permet de mettre une unité de mesure .
        /// </summary>
        /// <param name="category">The unite.</param>
        /// <returns></returns>
        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            var elementUpdated = _dBContext.Categories.Update(category);

            await _dBContext.SaveChangesAsync().ConfigureAwait(false);

            return elementUpdated.Entity;
        }
        /// <summary>
        /// Cette méthode permet de supprimer une unité de mesure.
        /// </summary>
        /// <param name="category">The unite.</param>
        /// <returns></returns>
        public async Task<Category> DeleteCategoryAsync(Category category)
        {
            var elementDeleted = _dBContext.Categories.Remove(category);
            await _dBContext.SaveChangesAsync().ConfigureAwait(false);
            return elementDeleted.Entity;
        }



    }
}
