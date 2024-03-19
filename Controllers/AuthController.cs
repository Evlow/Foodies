using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Foodies.Api.Data.Models;
using Foodies.Api.Buisness.DTO.Auth.SignUp;
using Foodies.Api.Buisness.DTO.Auth;
using Foodies.Api.Common.Models;
using Foodies.Api.Buisness.DTO.Auth.Login;
using Foodies.Api.Common.Services;
using Org.BouncyCastle.Bcpg;

namespace Foodies.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IEmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody] SignUpDTO registerUser)
        {
            // Récupération du rôle utilisateur à partir de la configuration
            string role = _configuration["Roles:user"];

            // Vérification de l'existence de l'utilisateur par son adresse e-mail
            var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
            if (userExist != null)
            {
                // Retourner une réponse interdite si l'utilisateur existe déjà
                return StatusCode(StatusCodes.Status403Forbidden, new Response { Status = "Error", Message = "User Already Exists" });
            }

            // Création d'un nouvel utilisateur
            User user = new()
            {
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUser.Username
            };

            // Vérification de l'existence du rôle dans la base de données
            if (await _roleManager.RoleExistsAsync(role))
            {
                // Création de l'utilisateur dans la base de données avec le mot de passe fourni
                var result = await _userManager.CreateAsync(user, registerUser.Password);
                if (!result.Succeeded)
                {
                    // Retourner une réponse d'erreur interne du serveur en cas d'échec de la création de l'utilisateur
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { Status = "Error", Message = "Le mot de passse doit contenir au moins 6 caractères dont au moins 1 majuscule et 1 caractère spécial." });
                }

                // Ajout du rôle à l'utilisateur nouvellement créé
                await _userManager.AddToRoleAsync(user, role);

                // Génération du token pour la vérification de l'adresse e-mail
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = user.Email }, Request.Scheme);

                // Envoi du lien de confirmation par e-mail à l'utilisateur
                var message = new Message(new string[] { user.Email! }, "Confirmation email link", confirmationLink!);
                _emailService.SendEmail(message);

                // Retourner une réponse OK avec un message de succès
                return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = $"User created & Email Sent to {user.Email} Successfully" });
            }
            else
            {
                // Retourner une réponse d'erreur interne du serveur si le rôle spécifié n'existe pas
                return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { Status = "Error", Message = "This Role does not exist" });
            }
        }


        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail(string token, string email)
        {
            // Recherche de l'utilisateur par son adresse e-mail
            var user = await _userManager.FindByEmailAsync(email);

            // Vérification si l'utilisateur existe
            if (user != null)
            {
                // Confirmation de l'e-mail de l'utilisateur en utilisant le token fourni
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    // Retourner une réponse OK avec un message de succès si la confirmation de l'e-mail réussit
                    return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Message = "Email verified Successfully" });
                }
            }

            // Retourner une réponse d'erreur interne du serveur si l'utilisateur n'existe pas
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Status = "Error", Message = "This User does not exist" });
        }


        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginModel)
        {
            // Vérification de l'utilisateur en utilisant le nom d'utilisateur fourni
            var user = await _userManager.FindByNameAsync(loginModel.Username);

            // Vérification du nom d'utilisateur et du mot de passe
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                // Création de la liste des revendications (claims)
                var authClaims = new List<Claim>
        {
            // Ajout du nom d'utilisateur
            new Claim(ClaimTypes.Name, user.UserName),
            // Ajout de l'identifiant de l'utilisateur
            new Claim(ClaimTypes.Sid, user.Id),
            // Génération d'un identifiant unique pour le jeton JWT
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

                // Ajout des rôles de l'utilisateur
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                // Génération du jeton JWT
                var jwtToken = GetToken(authClaims);

                // Retour du jeton JWT, de sa date d'expiration et de l'identifiant de l'utilisateur
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo,
                    userId = user.Id,
                });
            }

            // Retourner une réponse Unauthorized si l'authentification échoue
            return Unauthorized();
        }


        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> ForgotPassword([Required] string email)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);

        //    if (user != null)
        //    {
        //        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //        var forgotPasswordLink = Url.Action(
        //            "ResetPassword", "Authentication",
        //            new
        //            {
        //                token,
        //                email = user.Email
        //            },
        //            Request.Scheme);

        //        var message = new Message(
        //            new string[] {
        //                user.Email!
        //            },
        //            "Forgot Password link",
        //            forgotPasswordLink);

        //        _emailService.SendEmail(message);

        //        return StatusCode(StatusCodes.Status200OK,
        //            new Response
        //            {
        //                Status = "success",
        //                Message =
        //            $"Password changed request is sent on Email {user.Email}. Please open the email and click the link"
        //            });
        //    }

        //    return StatusCode(StatusCodes.Status400BadRequest,
        //            new Response
        //            {
        //                Status = "error",
        //                Message =
        //            "Could not send link to email, please try again."
        //            });
        //}

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            // Récupération de la clé de signature à partir de la configuration
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            // Création du jeton JWT avec les informations fournies
            var token = new JwtSecurityToken(
                // Émetteur du jeton (qui émet le jeton)
                issuer: _configuration["JWT:ValidIssuer"],
                // Auditeur du jeton (pour qui le jeton est destiné)
                audience: _configuration["JWT:ValidAudience"],
                // Date d'expiration du jeton (3 heures)
                expires: DateTime.Now.AddHours(3),
                // Revendications (informations associées au jeton)
                claims: authClaims,
                // Signature du jeton à l'aide de la clé de signature et de l'algorithme HMACSHA256
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
            );

            // Retour du jeton JWT créé
            return token;
        }

    }
}
