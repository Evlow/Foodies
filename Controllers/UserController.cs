﻿using Foodies.Api.Business.DTOs;
using Foodies.Api.Business.Services.interfaces;
using Foodies.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Foodies.Api.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// </summary>
        private readonly IUserService _userService;
        private readonly FoodiesDBContext _dbContext;

        public UserController(IUserService userService, FoodiesDBContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }




        // GET api/Unites
        /// <summary>
        ///  Le service de gestion des unités de mesure de mesure.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<UserDTO>), 200)]
        public async Task<ActionResult> GetUsersAsync()
        {
            var users = await _userService.GetUsersAsync().ConfigureAwait(false);

            return Ok(users);
        }

        ////GET api/Unites
        ///// <summary>
        ///// Ressource pour récupérer la un Id d'une unité de mesure
        //// / </summary>
        ////    / <returns></returns>
        //[HttpGet()]
        //[ProducesResponseType(typeof(UserDTO), 200)]
        //public async Task<ActionResult> GetUserIdAsync(string userId)
        //{
        //    try
        //    {
        //        var user = await _userService.GetUserIdAsync(userId).ConfigureAwait(false);

        //        return Ok(userId);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(new
        //        {
        //            Error = e.Message,
        //        });
        //    }

        //}

        ////GET api/Unites
        ////    / <summary>
        ////    / Ressource pour récupérer la un Id d'une unité de mesure
        ////    / </summary>
        ////    / <returns></returns>
        //[HttpGet]
        //[ProducesResponseType(typeof(UserDTO), 200)]
        //public async Task<ActionResult> GetUserByUserNameAsync(string UserName)
        //{
        //    try
        //    {
        //        var userName = await _userService.GetUserByUserNameAsync(UserName).ConfigureAwait(false);

        //        return Ok(userName);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(new
        //        {
        //            Error = e.Message,
        //        });
        //    }
        //}

        //GET api/Unites
        //    / <summary>
        //    / Ressource pour récupérer la un Id d'une unité de mesure
        //    / </summary>
        //    / <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(UserDTO), 200)]
        public async Task<ActionResult> GetUserByIdAsync(string userId)
        {
            var user = _dbContext.Users
                .Where(u => u.Id == userId)
                .Select(u => new UserDTO
                {
                    UserId = u.Id,
                    Email = u.Email,
                    UserName= u.UserName,
                })
                .FirstOrDefault();

            return Ok(user);
        }




        //POST api/Unites
        //    / <summary>
        //    / Ressource pour créer une nouvelle unité de mesure.
        //    / </summary>
        //    / <param name = "unity" > les données de l'unité à créer</param>
        //    / <returns></returns>
        [HttpPost]
            [ProducesResponseType(typeof(UserDTO), 200)]
            public async Task<ActionResult> CreateUserAsync([FromBody] UserDTO user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                return Problem("Echec : Ce nom est déjà utilisé !");
            }

            try
            {
                var userAdded = await _userService.CreateUserAsync(user).ConfigureAwait(false);

                return Ok(userAdded);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Error = e.Message,
                });
            }

        }

        //PUT api/Unites/1
        //    / <summary>
        //    / Ressource pour mettre à jour une unité de mesure.
        //    / </summary>
        //    / <param name = "id" > L'identifiant de l'unité.</param>
        //    / <param name = "unite" > les données modifiées.</param>
        //    / <returns></returns>
            [HttpPut("{id}")]
            [ProducesResponseType(typeof(UserDTO), 200)]
            public async Task<ActionResult> UpdateUserAsync(string id, [FromBody] UserDTO user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                return Problem("Echec : Ce nom est déjà utilisé !");
            }

            try
            {
                var userUpdated = await _userService.UpdateUserAsync(id, user).ConfigureAwait(false);

                return Ok(userUpdated);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Error = e.Message,
                });
            }

        }

        //DELETE api/Unites/1
        //    / <summary>
        //    / Ressource pour supprimer une unité de mesure.
        //    / </summary>
        //    / <param name = "id" > L'identifiant de l'unité.</param>
        //    / <returns></returns>
            [HttpDelete("{id}")]
        [ProducesResponseType(typeof(UserDTO), 200)]
        public async Task<ActionResult> DeleteUseryAsync(string id)
        {
            try
            {
                var userDeleted = await _userService.DeleteUserAsync(id).ConfigureAwait(false);

                return Ok(userDeleted);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Error = e.Message,
                });
            }

        }
    }

}
