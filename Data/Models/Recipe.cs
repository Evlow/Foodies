namespace Foodies.Api.Data.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string RecipeTitle { get; set; }

        // Foreign key property
        public string RecipeUserId { get; set; }


        // Navigation property
        public User User { get; set; }
    }
}
