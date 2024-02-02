using Foodies.Api.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Foodies.Api.Data.Models
{
    public class Favori
    {
        [Key]
        public int FavorisId { get; set; }
        public string? UserId { get; set; }
        public int? RecipeId { get; set; }

        public virtual Recipe? Recipe { get; set; }
        public virtual User? User { get; set; }
    }
}
