namespace Foodies.Api.Business.DTOs
{
    public class RecipeDTO
    {

        public int RecipeId { get; set; }
        public string RecipeUserName { get; set; }
        public string RecipeTitle { get;  set; }
        public int CategoryId { get; set; }
        public string? RecipePicture { get; set; }
        public DateTime? RecipeCreatedAt { get; set; }
        public DateTime? RecipeUpdatedAt { get; set; }
        public decimal? RecipeStarNote { get; set; }
    }
}
