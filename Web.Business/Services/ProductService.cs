using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web.Business.Extensions;
using Web.Business.Interfaces;
using Web.Contracts.Dtos.ImageDtos;
using Web.Contracts.Dtos.ProductDtos;
using Web.Contracts.Dtos.QueryDtos.ProductQueryDto;
using Web.Contracts.Exceptions;
using Web.Contracts.Models;
using Web.DataAccessor.Data;
using Web.DataAccessor.Entities;

namespace Web.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IBaseRepository<ProductsInCategories> _productInCateRepository;
        private readonly IBaseRepository<ProductImage> _productImageRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public ProductService(IBaseRepository<Product> productRepository,
        IBaseRepository<ProductsInCategories> productInCateRepository,
        IBaseRepository<ProductImage> productImageRepository,
        IMapper mapper, ApplicationDbContext context)
        {
            _productRepository = productRepository;
            _productInCateRepository = productInCateRepository;
            _productImageRepository = productImageRepository;
            _mapper = mapper;
            _context = context;
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

        public async Task<CommandResultModel<List<ProductDto>>> GetAllAsync()
        {
            try
            {
                var queryResult = _context.Products
                                    .Include(x => x.Images)
                                    .AsNoTracking()
                                    .AsQueryable();
                var data = _mapper.Map<List<ProductDto>>(queryResult);
                return new CommandResultModel<List<ProductDto>>
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

        public async Task<CommandResultModel<ProductDto>> GetByIdAsync(int id)
        {
            try
            {
                var queryResult = await _productRepository.GetByIdAsync(id);
                if (queryResult == null)
                {
                    throw new BadRequestException("Product is not exist");
                }
                var data = _mapper.Map<ProductDto>(queryResult);
                var listImages = await _productImageRepository.Entities
                                        .Where(x => x.ProductId == queryResult.Id).ToListAsync();
                data.Images = _mapper.Map<List<ImageDto>>(listImages);
                return new CommandResultModel<ProductDto>
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

        public async Task<CommandResultModel<PagedResponseModel<ProductDto>>> GetProductAsync(ProductQueryDto query)
        {
            try
            {
                var queryResult = _context.Products
                                    .Include(x => x.Images)
                                    .Include(x => x.ProductsInCategories)
                                    .AsNoTracking()
                                    .AsQueryable();
                if (query.CategoryId != null)
                {
                    queryResult = queryResult.Where(x => x.ProductsInCategories.Any(y => y.CategoryId == query.CategoryId));
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

        public async Task<CommandResultModel<List<ProductDto>>> GetSuggestionProduct(string keyword)
        {
            try
            {
                var queryResult = await _context.Products
                                    .Include(x => x.Images)
                                    .Where(x => x.Title.ToLower().Contains(keyword.ToLower()))
                                    .Take(10)
                                    .ToListAsync();

                var data = _mapper.Map<List<ProductDto>>(queryResult);
                return new CommandResultModel<List<ProductDto>>
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
    }
}