using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web.Business.Extensions;
using Web.Business.Interfaces;
using Web.Contracts.Dtos.ProductDtos;
using Web.Contracts.Dtos.QueryDtos.ProductQueryDto;
using Web.Contracts.Models;
using Web.DataAccessor.Data;
using Web.DataAccessor.Entities;

namespace Web.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IBaseRepository<ProductsInCategories> _productInCateRepository;
        private readonly IMapper _mapper;
        public ProductService(IBaseRepository<Product> productRepository,
        IBaseRepository<ProductsInCategories> productInCateRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _productInCateRepository = productInCateRepository;
            _mapper = mapper;
        }

        public async Task<CommandResultModel<bool>> CreateAsync(ProductCreateDto newProduct)
        {
            try
            {
                var product = _mapper.Map<Product>(newProduct);
                product.DateCreated = DateTime.Now;
                product.ProductsInCategories = new List<ProductsInCategories>();
                foreach (var cate in newProduct.CategoriesId)
                {
                    var newProInCate = new ProductsInCategories()
                    {
                        CategoryId = cate
                    };
                    product.ProductsInCategories.Add(newProInCate);
                };
                await _productRepository.Add(product);
                return new CommandResultModel<bool>
                {
                    StatusCode = (int)HttpStatusCode.Created,
                    Message = "Created success",
                    Data = true
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CommandResultModel<PagedResponseModel<ProductDto>>> GetProductAsync(int? categoryId, ProductQueryDto query)
        {
            try
            {
                var queryResult = _productRepository.Entities.AsNoTracking().AsQueryable();
                if (categoryId != null)
                {
                    var productsInCate = await _productInCateRepository.Entities
                                            .Where(x => x.CategoryId == categoryId)
                                            .Select(x => x.ProductId)
                                            .ToListAsync();
                    queryResult = queryResult.Where(x => productsInCate.Contains(x.Id));
                }

                var pagedResult = await queryResult
                                        .AsNoTracking()
                                        .PaginateAsync<Product>(query.PageIndex, query.PageSize);

                var data = _mapper.Map<List<ProductDto>>(pagedResult.Items);

                return new CommandResultModel<PagedResponseModel<ProductDto>>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Data = new PagedResponseModel<ProductDto>
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
    }
}