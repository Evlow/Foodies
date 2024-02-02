using Foodies.Api.Business.DTOs;

namespace Foodies.Api.Business.Services.interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Cette méthode permet de récupérer les listes des utilisateurs.
        /// </summary>
        /// <returns></returns>
        Task<List<UserDTO>> GetUsersAsync();

        /// <summary>
        /// Cette méthode permet de récupérer un id d'un utilisateur.
        /// </summary>
        /// <returns></returns>
        Task<UserDTO> GetUserIdAsync(string userId);

        /// <summary>
        /// Cette méthode permet de récupérer le pseudo d'un utilisateur.
        /// </summary>
        /// <returns></returns>
        Task<UserDTO> GetUserByUserNameAsync(string userId);

        /// <summary>
        /// Cette méthode permet de créer un utilisateur.
        /// </summary>
        /// <param name="user">L'utilisateur à créer.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Il existe déjà un utilisateur  du même nom !!</exception>
        Task<UserDTO> CreateUserAsync(UserDTO user);

        /// <summary>
        /// Cette méthode permet de mettre à jour un utilisateur  .
        /// </summary>
        /// <param name="userId">l'identifiant de utilisateur</param>
        /// <param name="user">l'utilisateur modifié</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// Il existe déjà un utilisateur  du même nom !!
        /// or
        /// Il n'existe aucun utilisateur  avec cet identifiant : {idUnite}
        /// </exception>
        Task<UserDTO> UpdateUserAsync(string userId, UserDTO user);

        /// <summary>
        /// Cette méthode permet de supprimer un utilisateur .
        /// </summary>
        /// <param name="userId">L'identifiant de l'utilisateur à supprimer.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Il n'existe aucun utilisateur  avec cet identifiant : {idUnite}</exception>
        Task<UserDTO> DeleteUserAsync(string userId);
    }
}
