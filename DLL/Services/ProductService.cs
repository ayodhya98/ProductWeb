using AutoMapper;
using DAL;
using DAL.Model;
using DLL.Dto;
using DLL.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Runtime.InteropServices;

namespace DLL.Services
{
    public class ProductService : IProductService   
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductService(ApplicationDbContext context,IMapper mapper) 
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public async Task<ProductDto> CreateProduct(CreateProductDto createProductDto)
        {
            ProductDto productDto = new ProductDto();

            if (createProductDto == null)
            {
                return  productDto;
            }

             Product product = new Product();
            product = _mapper.Map<Product>(createProductDto);

            _dbContext.Products.Add(product);
           await _dbContext.SaveChangesAsync();

            productDto = _mapper.Map<ProductDto>(product);

            return productDto;

        }

        public async Task<bool> DeleteProduct(int id)
        {
            

            if (id == 0)
            {
                return false;
            }

            var product = _dbContext.Products.FirstOrDefault(x => x.Id == id);
            if (product != null) 
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<ProductDto>> GetAllProducts()
        {

            var products = await _dbContext.Products.ToListAsync();

            var ProductsToReturn = _mapper.Map<List<ProductDto>>(products);
            
            return ProductsToReturn;
        }

        public async Task<ResponseDto<ProductDto>> GetProductById(int id)
        {
            try
            {
                ResponseDto<ProductDto> responseDto = new();
                var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                {
                    responseDto.IsSuccess = false;
                    responseDto.httpCode = 404;
                    responseDto.error = "Product Not Found";
                }
                else {
                    var productdto = _mapper.Map<ProductDto>(product);
                    responseDto.IsSuccess = true;
                    responseDto.httpCode = 200;
                    responseDto.data = productdto;
                   
                }
                return responseDto;
            }
            catch (Exception ex)
            {
                ResponseDto<ProductDto> responseDto = new();
                responseDto.IsSuccess = false;
                responseDto.httpCode = 400;
                responseDto.error = "bad request";
                //logger 
                return responseDto;
            }

  
           

        }
    }
}
