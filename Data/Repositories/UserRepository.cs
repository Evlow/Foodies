using Foodies.Api.Data.Models;
using Foodies.Api.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Foodies.Api.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        /// <summary>
        ///  Context de connexion à la base de données
        /// </summary>
        private readonly FoodiesDBContext _dBContext;

        public UserRepository(FoodiesDBContext dBContext)
        {
            _dBContext = dBContext ?? throw new ArgumentNullException(nameof(dBContext));
        }



        /// <summary>
        /// Cette méthode permet de récupérer la liste de toutes les unités de mesure.
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetUsersAsync()
        {
            return await _dBContext.Users.ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Cette méthode permet de récupérer les informations d'une unité de mesure par identifiant.
        /// </summary>
        /// <param name="id">L'identifiant.</param>
        /// <returns></returns>
        public async Task<User> GetUserByIdAsync(string id)
        {

            return await _dBContext.Users.FirstOrDefaultAsync(user => user.Id == id);

        }

        /// <summary>
        /// Cette méthode permet de récupérer les informations d'un utilisateur par son pseudo
        /// </summary>
        /// <param name="name">le nom de l'unité.</param>
        /// <returns></returns>
        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _dBContext.Users.FirstOrDefaultAsync(user => user.UserName == userName)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Cette méthode permet de récupérer les informations d'un utilisateur par email
        /// </summary>
        /// <param name="name">le nom de l'unité.</param>
        /// <returns></returns>
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dBContext.Users.FirstOrDefaultAsync(user => user.Email == email)
                .ConfigureAwait(false);
        }


        /// <summary>
        /// Cette méthode permet de créer une unité de mesure.
        /// </summary>
        /// <param name="unity">The unite.</param>
        /// <returns></returns>
        public async Task<User> CreateUserAsync(User user)
        {
            var elementAdded = await _dBContext.Users.AddAsync(user).ConfigureAwait(false);
            await _dBContext.SaveChangesAsync().ConfigureAwait(false);
            return elementAdded.Entity;
        }


        /// <summary>
        /// Cette méthode permet de mettre une unité de mesure .
        /// </summary>
        /// <param name="unity">The unite.</param>
        /// <returns></returns>
        public async Task<User> UpdateUserAsync(User user)
        {
            var elementUpdated = _dBContext.Users.Update(user);

            await _dBContext.SaveChangesAsync().ConfigureAwait(false);

            return elementUpdated.Entity;
        }
        /// <summary>
        /// Cette méthode permet de supprimer une unité de mesure.
        /// </summary>
        /// <param name="unity">The unite.</param>
        /// <returns></returns>
        public async Task<User> DeleteUserAsync(User user)
        {
            var elementDeleted = _dBContext.Users.Remove(user);
            await _dBContext.SaveChangesAsync().ConfigureAwait(false);
            return elementDeleted.Entity;
        }
    }
}
