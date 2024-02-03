using Foodies.Api.Data.Models;

namespace Foodies.Api.Business.DTOs
{
    public class UserDTO
    {
        public string? UserName { get; set; }
        public string UserId { get; set; }
        public string? Email { get; set; }

        //public List<Recipe> Recipes { get; set; }
        public IEnumerable<Recipe> GetRecipesByUserIdAsync()
        {
            // Filtrer les recettes en fonction de l'ID de l'utilisateur
            return Recipes.Where(recipe => recipe.UserId == UserId);
        }
        //public virtual ICollection<Comment> Comments { get; set; }
        //public virtual ICollection<Favori> Favoris { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }

    }
}
