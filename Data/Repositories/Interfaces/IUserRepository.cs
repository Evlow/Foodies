using Foodies.Api.Data.Models;

namespace Foodies.Api.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Cette méthode permet de récupérer la liste des utilisateurs
        /// <returns></returns>
        Task<List<User>> GetUsersAsync();

        /// <summary>
        /// Cette méthode permet de récupérer les informations d'un utilisateur par son identifiant
        /// </summary>
        /// <param name="id">L'identifiant.</param>
        /// <returns></returns>
        Task<User> GetUserByIdAsync(string id);

        /// <summary>
        /// Cette méthode permet de récupérer les informations d'un utilisateur par son nom.
        /// </summary>
        /// <param name="name">le nom de l'unité de mesure.</param>
        /// <returns></returns>
        Task<User> GetUserByUserNameAsync(string UserName);
        /// <summary>
        /// Cette méthode permet de récupérer les informations d'un utilisateur par son email.
        /// </summary>
        /// <param name="name">le nom de l'unité de mesure.</param>
        /// <returns></returns>
        Task<User> GetUserByEmailAsync(string email);

        /// <summary>
        /// Cette méthode permet de créer un utilisateur
        /// </summary>
        /// <param name="user">The unite.</param>
        /// <returns></returns>
        Task<User> CreateUserAsync(User user);

        /// <summary>
        /// Cette méthode permet de mettre à jour un utilisateur.
        /// </summary>
        /// <param name="user">The unite.</param>
        /// <returns></returns>
        Task<User> UpdateUserAsync(User user);


        /// <summary>
        /// Cette méthode permet de supprimer un utilisateur
        /// </summary>
        /// <param name="user">The unite.</param>
        /// <returns></returns>
        Task<User> DeleteUserAsync(User user);
    }
}
