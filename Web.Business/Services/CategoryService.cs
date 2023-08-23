using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Business.Interfaces;
using Web.Contracts.Dtos.CategoryDtos;
using Web.Contracts.Exceptions;
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

        public async Task<CategoryDto> CreateAsync(CategoryCreateDto newCategory)
        {
            try
            {
                var category = _mapper.Map<Category>(newCategory);
                category.DateCreated = DateTime.Now;
                var result = await _categoryRepository.Add(category);
                return _mapper.Map<CategoryDto>(category);
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }

        public async Task<CategoryDto> UpdateAsync(int id, CategoryUpdateDto updateCategory)
        {
            try
            {
                var category = await _categoryRepository.Entities.FirstOrDefaultAsync(x => x.CategoryId == id);
                if (category == null)
                {
                    throw new NotFoundException("Not found category!!!");
                }
                _mapper.Map(updateCategory, category);
                await _categoryRepository.Update(category);
                return _mapper.Map<CategoryDto>(category);
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var category = await _categoryRepository.Entities.FirstOrDefaultAsync(x => x.CategoryId == id);
                if (category == null)
                {
                    throw new NotFoundException("Not found category!!!");
                }
                var result = await _categoryRepository.Delete(category);
                if (!result)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }
    }
}
