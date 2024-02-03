using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foodies.Api.Data.Models
{
    public class Favori
    {
        [Key]
        public int FavorisId { get; set; }

        // Clés étrangères
        [ForeignKey("User")]
        public string? UserId { get; set; }

        [ForeignKey("Recipe")]
        public int? RecipeId { get; set; }

        // Navigation properties
        public virtual Recipe? Recipe { get; set; }
        public virtual User? User { get; set; }
    }
}
