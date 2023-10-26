using DAL.Model;
using DLL.Dto;
using DLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers


{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;


        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAllProducts()
        {
            return Ok(await _productService.GetAllProducts());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
           
            return Ok(await _productService.GetProductById(id));

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("createProduct")]

        public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto createProductDto)
        {

            return Ok(await _productService.CreateProduct(createProductDto));
        }

        [HttpDelete("{id:int}")]
         public async Task<ActionResult<bool>> DeleteProduct(int id)
        {
            return Ok(await _productService.DeleteProduct(id));
        }
    }
}
