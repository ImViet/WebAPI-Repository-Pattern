using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Business.Interfaces;
using Web.Contracts.Dtos.CategoryDtos;
using Web.Contracts.Dtos.QueryDtos.CategoryQueryDtos;
using Web.Contracts.Exceptions;
using Web.DataAccessor.Data;
using Web.DataAccessor.Entities;

namespace Web.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public CategoryService(IBaseRepository<Category> categoryRepository, IMapper mapper, ApplicationDbContext context)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _context = context;
        }
        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var listCategory = await _categoryRepository.Entities.ToListAsync();
            return _mapper.Map<List<CategoryDto>>(listCategory);
        }
        public async Task<List<CategoryDto>> GetPagingAsync(CategoryQueryDto query)
        {
            var listCategory = _categoryRepository.Entities.AsQueryable();
            if(!string.IsNullOrEmpty(query.Search))
            {
                listCategory = listCategory.Where(x => x.CategoryName.ToLower().Contains(query.Search.ToLower()));
            }
            var listCategoryResult = await listCategory
                            .Skip((query.PageIndex - 1) * query.PageSize)
                            .Take(query.PageSize)
                            .ToListAsync();
            return _mapper.Map<List<CategoryDto>>(listCategoryResult);
        }
        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if(category == null)
            {
                throw new NotFoundException("Not found category!!!");
            }
            return _mapper.Map<CategoryDto>(category);
        }
        public async Task<CategoryDto> CreateAsync(CategoryCreateDto newCategory)
        {
            var category = _mapper.Map<Category>(newCategory);
            category.DateCreated = DateTime.Now;
            var result = await _categoryRepository.Add(category);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateAsync(int id, CategoryUpdateDto updateCategory)
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

        public async Task<bool> DeleteAsync(int id)
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

        public async Task<List<CategoryDto>> GetAllWithSPAsync()
        {
            var listCate = await _context.Sp_GetAllCategory_Results.FromSqlRaw("Exec sp_GetAllCategory").ToListAsync();
            return _mapper.Map<List<CategoryDto>>(listCate);
        }
    }
}
