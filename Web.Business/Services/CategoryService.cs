using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Business.Interfaces;
using Web.Contracts.Dtos.CategoryDtos;
using Web.DataAccessor.Entities;

namespace Web.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(IBaseRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var listCategory = await _categoryRepository.Entities.ToListAsync();
            return _mapper.Map<List<CategoryDto>>(listCategory);
        }
    }
}
