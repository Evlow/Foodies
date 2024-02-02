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
    }
}
