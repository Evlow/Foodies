namespace Foodies.Api.Data.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string RecipeTitle { get; set; }
        public string? RecipePicture { get; set; }
        public DateTime? RecipeCreatedAt { get; set; }
        public DateTime? RecipeUpdatedAt { get; set; }
        public decimal? RecipeStarNote { get; set; }

        // Foreign key property
        public string RecipeUserName { get; set; }

        public int CategoryId { get; set; }
        public string? UserId { get; set; }

        // Navigation property
        public User User { get; set; }

        public  Category Category { get; set; }
        public virtual ICollection<Favori> Favoris { get; set; }
        public virtual ICollection<Preparation> Preparations { get; set; }
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }

    }
}
