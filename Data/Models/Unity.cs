using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Foodies.Api.Data.Models
{
    public class Unity
    {
        public Unity()
        {
            RecipeIngredients = new HashSet<RecipeIngredient>();
        }
        [Key]

        public int UnityId { get; set; }
        public string? UnityName { get; set; }

        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
