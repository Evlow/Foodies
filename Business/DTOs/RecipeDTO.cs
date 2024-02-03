namespace Foodies.Api.Business.DTOs
{
    public class RecipeDTO
    {
        public string? RecipeTitle { get; set; }
        public int RecipeId { get; set; }
        public string UserId { get; set; }

        public int? CategoryId { get; set; }
        public string? RecipePicture { get; set; }
        public DateTime? RecipeCreatedAt { get; set; }
        public DateTime? RecipeUpdatedAt { get; set; }
        public string? IngredientN1 { get; set; }
        public string? IngredientN2 { get; set; }
        public string? IngredientN3 { get; set; }
        public string? IngredientN4 { get; set; }
        public string? IngredientN5 { get; set; }
        public string? IngredientN6 { get; set; }
        public string? IngredientN7 { get; set; }
        public string? IngredientN8 { get; set; }

        public string? PreparationN1 { get; set; }
        public string? PreparationN2 { get; set; }
        public string? PreparationN3 { get; set; }
        public string? PreparationN4 { get; set; }
        public string? PreparationN5 { get; set; }
        public string? PreparationN6 { get; set; }
        public string? PreparationN7 { get; set; }
        public string? PreparationN8 { get; set; }

    }
}