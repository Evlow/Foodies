using Foodies.Api.Business.DTOs;
using Foodies.Api.Business.Services;
using Foodies.Api.Business.Services.Interfaces;
using Foodies.Api.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Security.Claims;
using System;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;
using Foodies.Api.Commun.ImageService;
using AutoMapper;

namespace Foodies.Api.Controllers
{

        [Route("api/[controller]/[action]")]
        [ApiController]
        public class RecipeController : ControllerBase
        {
            /// <summary>
            ///  Le service de gestion des recettes de mesure
            /// </summary>
            private readonly IFileService _fileService;
            private readonly IRecipeService _recipeService;
            private readonly UserManager<User> _userManager;
            private readonly ImageService _imageService;
            private readonly IMapper _mapper;

        public RecipeController(IFileService fileService, IRecipeService recipeService, UserManager<User> userManager,
            ImageService imageService, IMapper mapper)
        {
            _fileService = fileService;
            _recipeService = recipeService;
            _userManager = userManager;
            _imageService = imageService;
            _mapper = mapper;
        }



        //GET api/Recipes
        /// <summary>
        /// Ressource pour récupérer la liste des recettes.
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
        /// Ressource pour récupérer la un Id d'une recette
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Recipe), 200)]
        public async Task<ActionResult> GetRecipeByRecipeId(int id)
        {
            try
            {   
                var recipe = await _recipeService.GetRecipeIdAsync(id).ConfigureAwait(false);
                
                return Ok(recipe);

            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Error = e.Message,
                });
            }

        }
        // GET api/Recipes
        /// <summary>
        /// Ressource pour récupérer la un Id d'une recette
        /// </summary>
        /// <returns></returns>
        //[HttpGet("{id}/image")]
        //[AllowAnonymous]
        //[ProducesResponseType(typeof(FileResult), 200)]
        //public async Task<ActionResult> RecipeImageId(int id)
        //{
        //    try
        //    {
        //        var recipe = await _recipeService.GetRecipeIdAsync(id).ConfigureAwait(false);
        //        if (recipe.RecipePicture.Length <= 0)
        //        {
        //            throw new Exception("Err invalid image");
        //        }
        //        MemoryStream ms = new (recipe.RecipePicture, false);

        //        var result = new FileStreamResult(ms, "image/jpeg");
        //        return result;
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(new
        //        {
        //            Error = e.Message,
        //        });
        //    }

        //}
        // POST api/Recipes
        /// <summary>
        /// Ressource pour créer une nouvelle recette.
        /// </summary>
        /// <param name="Recipe">les données de la recette à créer</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(RecipeDTO), 200)]
        public async Task<ActionResult<RecipeDTO>> CreateRecipeAsync([FromForm] RecipeDTO recipeDto)
        {

            try
            {
                var recipe = _mapper.Map<Recipe>(recipeDto);

                if (string.IsNullOrWhiteSpace(recipe.RecipeTitle))
                {
                    return Problem("Echec : le titre de la recette ne peut pas être vide !");
                }

                if(recipeDto.RecipePicture != null)
                {
                    var imageResult = await _imageService.AddImageAsync(recipeDto.RecipePicture);

                    if (imageResult.Error != null)
                        return BadRequest(new ProblemDetails { Title = imageResult.Error.Message });

                    recipe.PictureUrl = imageResult.SecureUri.ToString();
                    recipe.PublicId = imageResult.PublicId;
                }

                var userSID = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid);
                if (userSID == null)
                {
                    return Problem("L'id est pas bon");
                }
                recipe.UserId = userSID.Value;

                var recipeAdded = await _recipeService.CreateRecipeAsync(recipe).ConfigureAwait(false);

                if (recipeAdded != null)
                {
                    return Ok(recipeAdded);
                }
                else
                {
                    return BadRequest(new { Error = "Erreur lors de l'ajout de la recette." });
                }
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
        public async Task<ActionResult<RecipeDTO>>UpdateRecipeAsync(int id, [FromForm] RecipeDTO recipeDto)
        {

            try
            {

                if (string.IsNullOrWhiteSpace(recipeDto.RecipeTitle))
                {
                    return Problem("Echec :le titre de la recette ne peut pas être vide !");
                }
                var recipe = _mapper.Map<Recipe>(recipeDto);
                if (recipeDto.RecipePicture != null)
                {
                    var imageResult = await _imageService.AddImageAsync(recipeDto.RecipePicture);

                    if (imageResult.Error != null)
                        return BadRequest(new ProblemDetails { Title = imageResult.Error.Message });

                    recipe.PictureUrl = imageResult.SecureUri.ToString();
                    recipe.PublicId = imageResult.PublicId;
                }


                var recipeUpdated = await _recipeService.updateRecipe(recipe);

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
