using Foodies.Api.Data.Models;

namespace Foodies.Api.Business.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string? RecipeUserName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }

        public IEnumerable<Recipe> GetRecipesAsync()
        {
            // Filtrer les recettes en fonction de l'ID de l'utilisateur
            return Recipes.Where(recipe => UserName == UserName);
        }
        //public virtual ICollection<Comment> Comments { get; set; }
        //public virtual ICollection<Favori> Favoris { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
