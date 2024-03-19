using Foodies.Api.Data.Models;

namespace Foodies.Api.Business.DTOs
{
    // DTO pour un utilisateur
    public class UserDTO
    {
        public string? UserName { get; set; }
        public string UserId { get; set; }
        public string? Email { get; set; }
        public IEnumerable<Recipe> GetRecipesByUserIdAsync()
        {
            // Filtrer les recettes en fonction de l'ID de l'utilisateur
            return Recipes.Where(recipe => recipe.UserId == UserId);
        }
        // Collection des recettes associées à cet utilisateur
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
