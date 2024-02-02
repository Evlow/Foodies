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

        /// <summary>
        /// Initializes a new instance of the <see cref="RecipeService"/> class.
        /// </summary>
        /// <param name="recipeRepository">The recipe repository.</param>
        public RecipeService(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;

        }

        /// <summary>
        /// Cette méthode permet de récupérer les listes des unités de mesure.
        /// </summary>
        /// <returns></returns>
        public async Task<List<RecipeDTO>> GetRecipesAsync()
        {
            var recipes = await _recipeRepository.GetRecipesAsync().ConfigureAwait(false);
            List<RecipeDTO> listRecipeDTO = new List<RecipeDTO>(recipes.Count);

            foreach (var recipe in recipes)
            {
                listRecipeDTO.Add(_mapper.Map<RecipeDTO>(recipe));
            }

            return listRecipeDTO;
        }


        public async Task<RecipeDTO> GetRecipeIdAsync(int recipeId)
        {
            var recipeGet = await _recipeRepository.GetRecipeByIdAsync(recipeId).ConfigureAwait(false);
            return _mapper.Map<RecipeDTO>(recipeGet);

        }

        /// <summary>
        /// Cette méthode permet de créer une recette.
        /// </summary>
        /// <param name="Recipe">La recette à créer.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Il existe déjà une recette avec ce nom !!</exception>
        public async Task<RecipeDTO> CreateRecipeAsync(RecipeDTO recipe)
        {
            var isExiste = await CheckRecipeTitleExisteAsync(recipe.RecipeTitle).ConfigureAwait(false);
            if (isExiste)
                throw new Exception("Il existe déjà une recette avec ce titre !");

            var recipeToAdd = _mapper.Map<Recipe>(recipe);

            var recipeAdded = await _recipeRepository.CreateRecipeAsync(recipeToAdd).ConfigureAwait(false);

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

        public async Task<List<RecipeDTO>> GetRecipesByUserNameAsync(string userName)
        {
            var recipesByUserName = await _recipeRepository.GetRecipesByUserNameAsync(userName).ConfigureAwait(false);
            List<RecipeDTO> listRecipeDTO = new List<RecipeDTO>(recipesByUserName.Count);

            foreach (var recipe in recipesByUserName)
            {
                listRecipeDTO.Add(_mapper.Map<RecipeDTO>(recipe));
            }

            return listRecipeDTO;
        }

        public async Task<List<RecipeDTO>> GetRecipesByCategoryIdAsync(int categoryId)
        {
            var recipesByCategory = await _recipeRepository.GetRecipesByCategoryIdAsync(categoryId).ConfigureAwait(false);
            List<RecipeDTO> listRecipeDTO = new List<RecipeDTO>(recipesByCategory.Count);

            foreach (var recipe in recipesByCategory)
            {
                listRecipeDTO.Add(_mapper.Map<RecipeDTO>(recipe));
            }

            return listRecipeDTO;
        }


        /// <summary>
        /// Cette méthode permet de mettre à jour une unité de mesure .
        /// </summary>
        /// <param name="UnityId">l'identifiant de unité</param>
        /// <param name="unity">l'unité modifié</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// Il existe déjà une unité de mesure du même nom !!
        /// or
        /// Il n'existe aucune unité de mesure avec cet identifiant : {UnityId}
        /// </exception>
        public async Task<RecipeDTO> UpdateRecipeAsync(int recipeId, RecipeDTO recipe)
        {
            var isExiste = await CheckRecipeTitleExisteAsync(recipe.RecipeTitle).ConfigureAwait(false);
            if (isExiste)
                throw new Exception("Il existe déjà une recette du même nom !!!");

            var recipeGet = await _recipeRepository.GetRecipeByIdAsync(recipeId).ConfigureAwait(false);
            if (recipeGet == null)
                throw new Exception($"Il n'existe aucune recette avec cet identifiant : {recipeId}");

            recipeGet.RecipeTitle = recipe.RecipeTitle;

            var recipeUpdated = await _recipeRepository.UpdateRecipeAsync(recipeGet).ConfigureAwait(false);

            return _mapper.Map<RecipeDTO>(recipeUpdated);
        }

        /// <summary>
        /// Cette méthode permet de supprimer une unité de mesure.
        /// </summary>
        /// <param name="recipeId">L'identifiant de l'unité à supprimer.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Il n'existe aucune unité de mesure avec cet identifiant : {idUnite}</exception>
        public async Task<RecipeDTO> DeleteRecipeAsync(int recipeId)
        {
            var recipeGet = await _recipeRepository.GetRecipeByIdAsync(recipeId).ConfigureAwait(false);
            if (recipeGet == null)
                throw new Exception($"Il n'existe aucune unité de mesure avec cet identifiant : {recipeId}");

            var recipeDeleted = await _recipeRepository.DeleteRecipeAsync(recipeGet).ConfigureAwait(false);

            return _mapper.Map<RecipeDTO>(recipeDeleted);
        }













    }
}
