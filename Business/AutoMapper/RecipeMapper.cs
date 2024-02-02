

using Foodies.Api.Business.DTOs;
using Foodies.Api.Data.Models;

namespace Foodies.Api.Business
{
    public static class RecipeMapper
    {
        public static Recipe TransformDTOToEntity(RecipeDTO recipeDTO)
        {
            return new Recipe()
            {

                RecipeTitle = recipeDTO.RecipeTitle,
                RecipePicture = recipeDTO.RecipePicture,
                RecipeCreatedAt = recipeDTO.RecipeCreatedAt,
                RecipeUpdatedAt = recipeDTO.RecipeUpdatedAt,
                RecipeStarNote = recipeDTO.RecipeStarNote
            };
        }

        public static RecipeDTO TransformEntityToDTO(Recipe recipeEntity)
        {
            return new RecipeDTO()
            {
                RecipeId = recipeEntity.RecipeId,
                RecipeUserName = recipeEntity.UserName,
                CategoryId = recipeEntity.CategoryId,
                RecipeTitle = recipeEntity.RecipeTitle,
                RecipePicture = recipeEntity.RecipePicture,
                RecipeCreatedAt = recipeEntity.RecipeCreatedAt,
                RecipeUpdatedAt = recipeEntity.RecipeUpdatedAt,
                RecipeStarNote = recipeEntity.RecipeStarNote
            };
        }
    }
}
