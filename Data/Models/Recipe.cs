using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;

namespace Foodies.Api.Data.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }
        public string RecipeTitle { get; set; }
        public string PictureUrl { get; set; }

        public string? PublicId { get; set; }

        public DateTime? RecipeCreatedAt { get; set; }
        public DateTime? RecipeUpdatedAt { get; set; }
        public decimal? RecipeStarNote { get; set; }

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

        [NotMapped]

        public IFormFile ImageFile { get; set; }
        // Foreign Key for UserId
        [ForeignKey("User")]
        public string UserId { get; set; }

        // Navigation properties
        public User User { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        // Other navigation properties
        public Category Category { get; set; }
    }
}
