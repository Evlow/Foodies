using Foodies.Api.Business.DTOs;
using Foodies.Api.Business.Services;
using Foodies.Api.Business.Services.Interfaces;
using Foodies.Api.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Foodies.Api.Controllers
{

        [Route("api/[controller]/[action]")]
        [ApiController]
        public class RecipeController : ControllerBase
        {
            /// <summary>
            ///  Le service de gestion des recettes de mesure
            /// </summary>
            private readonly IRecipeService _recipeService;
            private readonly UserManager<User> _userManager;

        public RecipeController(IRecipeService recipeService, UserManager<User> userManager, ILogger<RecipeController> logger)
        {
            _recipeService = recipeService ?? throw new ArgumentNullException(nameof(recipeService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }





        //GET api/Recipes
        /// <summary>
        /// Ressource pour récupérer la liste des recettes de mesure.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<RecipeDTO>), 200)]
        public async Task<ActionResult> GetRecipesAsync()
        {
            var recipes = await _recipeService.GetRecipesAsync().ConfigureAwait(false);

            return Ok(recipes);
        }

        // GET api/Recipes
        /// <summary>
        /// Ressource pour récupérer la un Id d'une recette de mesure
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(RecipeDTO), 200)]
        public async Task<ActionResult> RecipeId(int id)
        {
            try
            {
                var recipeId = await _recipeService.GetRecipeIdAsync(id).ConfigureAwait(false);

                return Ok(recipeId);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Error = e.Message,
                });
            }

        }

        // POST api/Recipes
        /// <summary>
        /// Ressource pour créer une nouvelle recette de mesure.
        /// </summary>
        /// <param name="Recipe">les données de l'recette à créer</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(RecipeDTO), 200)]
            public async Task<ActionResult> CreateRecipeAsync([FromBody] RecipeDTO recipe)
            {

                if (string.IsNullOrWhiteSpace(recipe.RecipeTitle))
                {
                    return Problem("Echec : le titre de la recette ne peut pas être vide !");
                }

                try
                {
                    var recipeAdded = await _recipeService.CreateRecipeAsync(recipe).ConfigureAwait(false);

                    return Ok(recipeAdded);
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        Error = e.Message,
                    });
                }

            }

        // PUT api/Recipes/1
        /// <summary>
        /// Ressource pour mettre à jour une recette de mesure.
        /// </summary>
        /// <param name="id">L'identifiant de l'recette.</param>
        /// <param name="Recipe">les données modifiées.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(RecipeDTO), 200)]
        public async Task<ActionResult> UpdateRecipeAsync(int id, [FromBody] RecipeDTO recipe)
        {
            if (string.IsNullOrWhiteSpace(recipe.RecipeTitle))
            {
                return Problem("Echec :le titre de la recette ne peut pas être vide !");
            }

            try
            {
                var recipeUpdated = await _recipeService.UpdateRecipeAsync(id, recipe).ConfigureAwait(false);

                return Ok(recipeUpdated);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Error = e.Message,
                });
            }

        }

        // DELETE api/Recipes/1
        /// <summary>
        /// Ressource pour supprimer une recette de mesure.
        /// </summary>
        /// <param name="id">L'identifiant de l'recette.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(RecipeDTO), 200)]
        public async Task<ActionResult> DeleteRecipeAsync(int id)
        {
            try
            {
                var recipeDeleted = await _recipeService.DeleteRecipeAsync(id).ConfigureAwait(false);

                return Ok(recipeDeleted);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Error = e.Message,
                });
            }

        }
        [HttpGet("{categoryId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<RecipeDTO>), 200)]
        public async Task<ActionResult> GetRecipesByCategoryIdAsync(int categoryId)
        {
            try
            {
                var recipesByCategory = await _recipeService.GetRecipesByCategoryIdAsync(categoryId).ConfigureAwait(false);

                return Ok(recipesByCategory);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Error = e.Message,
                });
            }

        }
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<RecipeDTO>), 200)]
        public async Task<IActionResult> GetRecipesByUserIdAsync(string userId)
        {
            Console.WriteLine("current Id : " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var recipesByUserId = await _recipeService.GetRecipesByUserIdAsync(userId).ConfigureAwait(false);

                return Ok(recipesByUserId);


        }

        }


    }
