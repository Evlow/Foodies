using AutoMapper;
using Foodies.Api.Business.DTOs;
using Foodies.Api.Data.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Foodies.Api.AutoMapper
{
    public class ConverFile {
        public byte[] Convert(RecipeDTO recipe)
        {
            var fileBase = recipe.RecipePicture;

            MemoryStream target = new MemoryStream();
            fileBase.OpenReadStream().CopyTo(target);
            return target.ToArray();
        }

    }
    public class AutoMapper : Profile
        {
            public AutoMapper()
            {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Recipe, RecipeDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
    }

