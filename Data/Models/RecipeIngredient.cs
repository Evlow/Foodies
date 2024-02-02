using Foodies.Api.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Foodies.Api.Data.Models
{
    public class RecipeIngredient
    {
        [Key]
        public int RecipeIngredienId { get; set; }


        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public int? UnityId { get; set; }
        public decimal? RecipeIngredientQuantity { get; set; }

        public virtual Ingredient Ingredient { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
        public virtual Unity? Unity { get; set; }
    }
}
