using AutoMapper;
using Foodies.Api.Business.DTOs;
using Foodies.Api.Business.Services.interfaces;
using Foodies.Api.Data.Models;
using Foodies.Api.Data.Repositories.Interfaces;

namespace Foodies.Api.Business.Services
{
    public class UserService : IUserService
    {
        /// <summary>
        /// Le repository de gestion des unités de mesures
        /// </summary>
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="unityRepository">The unite repository.</param>
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;

        }

        /// <summary>
        /// Cette méthode permet de récupérer les listes users.
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserDTO>> GetUsersAsync()
        {
            var users = await _userRepository.GetUsersAsync().ConfigureAwait(false);
            List<UserDTO> listUserDTO = new List<UserDTO>(users.Count);

            foreach (var user in users)
            {
                listUserDTO.Add(_mapper.Map<UserDTO>(user));

            }

            return listUserDTO;
        }


        public async Task<UserDTO> GetUserIdAsync(string userId)
        {
            var userGet = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);

            return _mapper.Map<UserDTO>(userGet);
        }


        public async Task<UserDTO> GetUserByUserNameAsync(string userName)
        {
            var userGetbyName = await _userRepository.GetUserByUserNameAsync(userName).ConfigureAwait(false);

            return _mapper.Map<UserDTO>(userGetbyName);
        }
        /// <summary>
        /// Cette méthode permet de créer une unité de mesure.
        /// </summary>
        /// <param name="unity">L'unité à créer.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Il existe déjà une unité de mesure du même nom !!</exception>
        public async Task<UserDTO> CreateUserAsync(UserDTO userDTO)
        {

            var isExiste = await CheckUserNameExisteAsync(userDTO.UserName).ConfigureAwait(false);
            if (isExiste)
                throw new Exception("Il existe déjà un utilisateur avec ce nom !!");

            var userToAdd = _mapper.Map<User>(userDTO);
            var userAdded = await _userRepository.CreateUserAsync(userToAdd).ConfigureAwait(false);

            return _mapper.Map<UserDTO>(userAdded);
        }


        /// <summary>
        /// Cette méthode permet de mettre à jour une unité de mesure .
        /// </summary>
        /// <param name="UnityId">l'identifiant de unité</param>
        /// <param name="unity">l'unité modifié</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// Il existe déjà une unité de mesure du même nom !!
        /// or
        /// Il n'existe aucune unité de mesure avec cet identifiant : {UnityId}
        /// </exception>
        public async Task<UserDTO> UpdateUserAsync(string userId, UserDTO user)
        {

            var isExiste = await CheckUserNameExisteAsync(user.UserName).ConfigureAwait(false);
            if (isExiste)
                throw new Exception("Il existe déjà une unité de mesure du même nom !!");

            var userGet = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);
            if (userGet == null)
                throw new Exception($"Il n'existe aucune unité de mesure avec cet identifiant : {userId}");

            userGet.UserName = user.UserName;

            var userUpdated = await _userRepository.UpdateUserAsync(userGet).ConfigureAwait(false);

            return _mapper.Map<UserDTO>(userUpdated);
        }

        /// <summary>
        /// Cette méthode permet de supprimer une unité de mesure.
        /// </summary>
        /// <param name="userId">L'identifiant de l'unité à supprimer.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Il n'existe aucune unité de mesure avec cet identifiant : {idUnite}</exception>
        public async Task<UserDTO> DeleteUserAsync(string userId)
        {
            var userGet = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);
            if (userGet == null)
                throw new Exception($"Il n'existe aucune unité de mesure avec cet identifiant : {userId}");

            var userDeleted = await _userRepository.DeleteUserAsync(userGet).ConfigureAwait(false);

            return _mapper.Map<UserDTO>(userDeleted);
        }




        /// <summary>
        /// Cette méthode permet de vérifier si une unité existe déjà avec le même nom.
        /// </summary>
        /// <param name="UserName">le nom de l'unité.</param>
        private async Task<bool> CheckUserNameExisteAsync(string UserName)
        {
            var userGet = await _userRepository.GetUserByUserNameAsync(UserName).ConfigureAwait(false);

            return userGet != null;
        }




    }
}