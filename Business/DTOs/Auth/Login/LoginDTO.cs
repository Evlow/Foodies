using System.ComponentModel.DataAnnotations;

namespace Foodies.Api.Buisness.DTO.Auth.Login
{
    public class LoginDTO

    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }

}
