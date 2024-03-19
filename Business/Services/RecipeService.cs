using AutoMapper;
using Foodies.Api.Business.DTOs;
using Foodies.Api.Business.Services.Interfaces;
using Foodies.Api.Data.Models;
using Foodies.Api.Data.Repositories.Interfaces;

namespace Foodies.Api.Business.Services
{
    public class RecipeService : IRecipeService
    {

        /// <summary>
        /// Le repository de gestion des recettes.
        /// </summary>
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RecipeService> _logger;


        /// <summary>
        /// Initializes a new instance of the <see cref="RecipeService"/> class.
        /// </summary>
        /// <param name="recipeRepository">The recipe repository.</param>
        public RecipeService(IRecipeRepository recipeRepository, IMapper mapper, ILogger<RecipeService> logger )
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _logger = logger;


        }

        /// <summary>
        /// Cette méthode permet de récupérer les listes des recettes.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Recipe>> GetRecipesAsync()
        {
            var recipes = await _recipeRepository.GetRecipesAsync().ConfigureAwait(false);
           return recipes;
        }


        /// <summary>
        /// Cette méthode permet de créer une recette.
        /// </summary>
        /// <param name="Recipe">La recette à créer.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Il existe déjà une recette avec ce titre !!</exception>
        public async Task<RecipeDTO> CreateRecipeAsync(Recipe recipe)
        {

            var isExiste = await CheckRecipeTitleExisteAsync(recipe.RecipeTitle).ConfigureAwait(false);
            if (isExiste)
                throw new Exception("Il existe déjà une recette avec ce titre !");

            var recipeAdded = await _recipeRepository.CreateRecipeAsync(recipe).ConfigureAwait(false);

            return _mapper.Map<RecipeDTO>(recipeAdded);
        }


        /// <summary>
        /// Cette méthode permet de vérifier si une recette existe déjà avec le même titre.
        /// </summary>
        /// <param name="recipeName">Le titre de la recette.</param>
        private async Task<bool> CheckRecipeTitleExisteAsync(string recipeTitle)
        {
            var recipeGet = await _recipeRepository.GetRecipeByTitleAsync(recipeTitle).ConfigureAwait(false);
            return recipeGet != null;
        }

        public async Task<List<Recipe>> GetRecipesByUserIdAsync(string userId)
        {
            var recipesByUserId = await _recipeRepository.GetRecipesByUserIdAsync(userId).ConfigureAwait(false);
            return recipesByUserId;
        }

        public async Task<List<Recipe>> GetRecipesByCategoryIdAsync(int categoryId)
        {
            var recipesByCategory = await _recipeRepository.GetRecipesByCategoryIdAsync(categoryId).ConfigureAwait(false);
            return recipesByCategory;
        }


        /// <summary>
        /// Cette méthode permet de mettre à une recette.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// </exception>
        public async Task<RecipeDTO> updateRecipe(Recipe recipe)
        {
            var recipeUpdated = await _recipeRepository.UpdateRecipeAsync(recipe).ConfigureAwait(false);

            return _mapper.Map<RecipeDTO>(recipeUpdated);
        }

        /// <summary>
        /// Cette méthode permet de supprimer une recette.
        /// </summary>
        /// <param name="recipeId">L'identifiant de la recette à supprimer.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Il n'existe aucune recette avec cet identifiant.</exception>
        public async Task<RecipeDTO> DeleteRecipeAsync(int recipeId)
        {
            var recipeGet = await _recipeRepository.GetRecipeByIdAsync(recipeId).ConfigureAwait(false);
            if (recipeGet == null)
                throw new Exception($"Il n'existe aucune recette avec cet identifiant : {recipeId}");

            var recipeDeleted = await _recipeRepository.DeleteRecipeAsync(recipeGet).ConfigureAwait(false);

            return _mapper.Map<RecipeDTO>(recipeDeleted);
        }
        public async Task<Recipe> GetRecipeIdAsync(int recipeId)
        {
            var recipeGet = await _recipeRepository.GetRecipeByIdAsync(recipeId).ConfigureAwait(false);
            return recipeGet;

        }

    }
}










