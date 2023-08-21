using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Contracts.Dtos.CategoryDtos;
using Web.DataAccessor.Entities;

namespace Web.Business
{
    public class AutoMapperProfile: AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            FromPresentationLayer();
            FromDataAccessorLayer();
        }
        private void FromPresentationLayer()
        {

        }
        private void FromDataAccessorLayer()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}
