using Foodies.Api.Business.DTOs;
using Foodies.Api.Data.Models;

namespace Foodies.Api.Business.AutoMapper
{
    public static class UserMapper
    {
        public static User TransformDTOToEntity(UserDTO userDTO)
        {
            return new User()
            {
                Id = userDTO.Id,
                UserName = userDTO.UserName,
                Email = userDTO.Email,
                PasswordHash = userDTO.PasswordHash
            };
        }

        public static UserDTO TransformEntityToDTO(User userEntity)
        {
            return new UserDTO()
            {
                Id = userEntity.Id,
                UserName = userEntity.UserName,
                Email = userEntity.Email,
                PasswordHash = userEntity.PasswordHash
            };
        }
    }
}
