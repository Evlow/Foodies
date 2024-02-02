

using System.ComponentModel.DataAnnotations;

namespace Foodies.Api.Data.Models
{
    public class Ingredient
    {
        public Ingredient()
        {
            RecipeIngredients = new HashSet<RecipeIngredient>();
        }
        [Key]

        public int IngredientId { get; set; }
        public string? IngredientName { get; set; }

        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
