﻿using AutoMapper;
using Foodies.Api.Business.DTOs;
using Foodies.Api.Data.Models;

namespace Foodies.Api.AutoMapper
{

        public class AutoMapper : Profile
        {
            public AutoMapper()
            {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            //CreateMap<Comment, CommentDTO>().ReverseMap();
            //CreateMap<Favori, FavoriDTO>().ReverseMap();
            CreateMap<Recipe, RecipeDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
    }

