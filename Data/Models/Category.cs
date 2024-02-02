using Foodies.Api.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Foodies.Api.Data.Models
{
    public class Category
    {
        public Category()
        {
            Recipes = new HashSet<Recipe>();
        }
        [Key]

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
