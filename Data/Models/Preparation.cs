using Foodies.Api.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Foodies.Api.Data.Models
{
    public class Preparation
    {
        [Key]

        public int PreparationId { get; set; }
        public int RecipeId { get; set; }
        public int PreparationEtape { get; set; }
        public string? PreparationDescription { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;
    }
}
