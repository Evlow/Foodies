using Microsoft.AspNetCore.Identity;

namespace Foodies.Api.Data.Models
{
    public class User : IdentityUser
    {

        public List<Recipe> Recipes { get; set; }

    // Navigation property

    //public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
