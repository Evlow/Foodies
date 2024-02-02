
using Foodies.Api.Business.DTOs;
using Foodies.Api.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodies.Api.Business
{
    public static class CategoryMapper
    {
        public static Category TransformDTOToEntity(CategoryDTO categoryDTO)
        {
            return new Category()
            {
                CategoryName = categoryDTO.CategoryName
            };
        }

        public static CategoryDTO TransformEntityToDTO(Category categoryEntity)
        {
            return new CategoryDTO()
            {
                CategoryId = categoryEntity.CategoryId,
                CategoryName = categoryEntity.CategoryName
            };
        }

    }

}

