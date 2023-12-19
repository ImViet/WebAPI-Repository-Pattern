using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Web.Business.Extensions;
using Web.Business.Interfaces;
using Web.Contracts.Dtos.CategoryDtos;
using Web.Contracts.Dtos.QueryDtos.CategoryQueryDtos;
using Web.Contracts.Exceptions;
using Web.Contracts.Models;
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
        public async Task<CommandResultModel<List<CategoryDto>>> GetAllAsync()
        {
            try
            {
                var listCategory = await _categoryRepository.Entities.ToListAsync();
                var data = _mapper.Map<List<CategoryDto>>(listCategory);
                return new CommandResultModel<List<CategoryDto>>()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Data = data
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<CommandResultModel<PagedResponseModel<CategoryDto>>> GetPagingAsync(CategoryQueryDto query)
        {
            try
            {
                var listCategory = _categoryRepository.Entities.AsQueryable();
                if (!string.IsNullOrEmpty(query.Search))
                {
                    listCategory = listCategory.Where(x => x.CategoryName.ToLower().Contains(query.Search.ToLower()));
                }

                var pagedResult = await listCategory
                                        .AsNoTracking()
                                        .PaginateAsync<Category>(query.PageIndex, query.PageSize);

                var data = _mapper.Map<List<CategoryDto>>(pagedResult.Items);

                return new CommandResultModel<PagedResponseModel<CategoryDto>>()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Data = new PagedResponseModel<CategoryDto>
                    {
                        CurrentPage = pagedResult.CurrentPage,
                        TotalItems = pagedResult.TotalItems,
                        TotalPages = pagedResult.TotalPages,
                        Items = data
                    }
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<CommandResultModel<CategoryDto>> GetByIdAsync(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    throw new NotFoundException("Not found category!!!");
                }
                var data = _mapper.Map<CategoryDto>(category);
                return new CommandResultModel<CategoryDto>()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Data = data
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<CommandResultModel<bool>> CreateAsync(CategoryCreateDto newCategory)
        {
            try
            {
                var category = _mapper.Map<Category>(newCategory);
                category.DateCreated = DateTime.Now;
                await _categoryRepository.Add(category);
                return new CommandResultModel<bool>()
                {
                    StatusCode = (int)HttpStatusCode.Created,
                    Message = "Create success",
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CommandResultModel<bool>> UpdateAsync(CategoryUpdateDto updateCategory)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(updateCategory.Id);
                if (category == null)
                {
                    throw new NotFoundException("Not found category!!!");
                }
                _mapper.Map(updateCategory, category);
                await _categoryRepository.Update(category);
                return new CommandResultModel<bool>()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Update success",
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CommandResultModel<bool>> DeleteAsync(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    throw new NotFoundException("Not found category!!!");
                }
                await _categoryRepository.Delete(category);
                return new CommandResultModel<bool>()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Delete success"
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
