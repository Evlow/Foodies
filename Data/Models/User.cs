using Microsoft.AspNetCore.Identity;

namespace Foodies.Api.Data.Models
{
    public class User : IdentityUser
    {
        public List<Recipe> Recipes { get; set; }

    }
}
