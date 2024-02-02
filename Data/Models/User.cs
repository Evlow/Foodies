using Microsoft.AspNetCore.Identity;

namespace Foodies.Api.Data.Models
{
    public class User : IdentityUser
    {
        public DateTime Ddn { get; set; }

        public ICollection<Recipe> Recipe { get; set; } = new List<Recipe>();
    }
}
