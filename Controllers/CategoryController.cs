using Foodies.Api.Business.DTOs;
using Foodies.Api.Business.Services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Foodies.Api.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        /// <summary>
        ///  Le service de gestion des categories de mesure
        /// </summary>
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityController"/> class.
        /// </summary>
        /// <param name="unityService">The category service.</param>
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET api/categorys
        /// <summary>
        /// Ressource pour récupérer la liste des categories de mesure.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryDTO>), 200)]
        public async Task<ActionResult> GetCategoriesAsync()
        {
            var categorys = await _categoryService.GetCategoriesAsync().ConfigureAwait(false);

            return Ok(categorys);
        }

        // GET api/categorys
        /// <summary>
        /// Ressource pour récupérer la un Id d'une categorie de mesure
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryDTO), 200)]
        public async Task<ActionResult> GetCategoryIdAsync(int id)
        {
            try
            {
                var categoryId = await _categoryService.GetCategoryIdAsync(id).ConfigureAwait(false);

                return Ok(categoryId);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Error = e.Message,
                });
            }

        }

        // POST api/categorys
        /// <summary>
        /// Ressource pour créer une nouvelle categorie de mesure.
        /// </summary>
        /// <param name="unity">les données de l'categorie à créer</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CategoryDTO), 200)]
        public async Task<ActionResult> CreateCategoryAsync([FromBody] CategoryDTO category)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                return Problem("Echec : Le nom de la catégorie est vide");
            }

            try
            {
                var categoryAdded = await _categoryService.CreateCategoryAsync(category).ConfigureAwait(false);

                return Ok(categoryAdded);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Error = e.Message,
                });
            }

        }

        // PUT api/categorys/1
        /// <summary>
        /// Ressource pour mettre à jour une categorie de mesure.
        /// </summary>
        /// <param name="id">L'identifiant de l'categorie.</param>
        /// <param name="category">les données modifiées.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CategoryDTO), 200)]
        public async Task<ActionResult> UpdateCategoryAsync(int id, [FromBody] CategoryDTO category)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                return Problem("Echec : Il existe déjà une catégorie avec ce nom.");
            }

            try
            {
                var categoryUpdated = await _categoryService.UpdateCategoryAsync(id, category).ConfigureAwait(false);

                return Ok(categoryUpdated);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Error = e.Message,
                });
            }

        }

        // DELETE api/categorys/1
        /// <summary>
        /// Ressource pour supprimer une categorie de mesure.
        /// </summary>
        /// <param name="id">L'identifiant de l'categorie.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CategoryDTO), 200)]
        public async Task<ActionResult> DeleteCategoryAsync(int id)
        {
            try
            {
                var categoryDeleted = await _categoryService.DeleteCategoryAsync(id).ConfigureAwait(false);

                return Ok(categoryDeleted);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Error = e.Message,
                });
            }

        }
    }
}

