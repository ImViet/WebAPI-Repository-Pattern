using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Contracts.Dtos.AuthDtos;
using Web.Contracts.Dtos.CategoryDtos;
using Web.Contracts.Dtos.ImageDtos;
using Web.Contracts.Dtos.ProductDtos;
using Web.Contracts.Dtos.UserDtos;
using Web.DataAccessor.Entities;

namespace Web.Business
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            FromPresentationLayer();
            FromDataAccessorLayer();
        }
        private void FromPresentationLayer()
        {
            //Mapping category from UI to entity server
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            //Mapping user from UI to entity server
            CreateMap<UserRegisterDto, User>();
            //Mapping product from UI to entity server
            CreateMap<ProductCreateDto, Product>();
        }
        private void FromDataAccessorLayer()
        {
            //Mapping category from data layer to UI
            CreateMap<Category, CategoryDto>();
            //Mapping user from data layer to UI
            CreateMap<User, UserDto>();
            CreateMap<User, AccountDto>();
            //Mapping product from data layer to UI
            CreateMap<Product, ProductDto>();
            //Mapping product image from data layer to UI
            CreateMap<ProductImage, ImageDto>();
        }
    }
}
