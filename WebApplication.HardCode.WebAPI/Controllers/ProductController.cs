using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication.HardCode.WebApi.Domain.Enum;
using WebApplication.HardCode.WebApi.Domain.ViewModel;
using WebApplication.HardCode.WebAPI.Service.Implementation;
using WebApplication.HardCode.WebAPI.Service.Interfaces;

namespace WebApplication.HardCode.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController:Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService=productService;
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _productService.AddProduct(productViewModel);
            if (response.StatusCode==StatusCodeEnum.OK)
            {
                return Ok("Successfully created");
            }
            return StatusCode(500);
        }
        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await _productService.GetAllProducts();
            if (response.StatusCode==StatusCodeEnum.NotFound)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(response.Data);
        }

        [HttpGet]
        [Route("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var response = await _productService.GetProductByIdAsync(id);
            if (response.StatusCode==StatusCodeEnum.NotFound)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(response.Data);
        }

        [HttpPost]
        [Route("DeleteProductById/{id}")]
        public async Task<IActionResult> DeleteProductById(int id)
        {
            var response = await _productService.DeleteProduct(id);
            if (response.StatusCode==StatusCodeEnum.NotFound)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(response.Description);
        }

        [HttpPut]
        [Route("UpdateProductById/{id}")]
        public async Task<IActionResult> UpdateCategoryByyId(int id, [FromBody] ProductViewModel productViewModel)
        {

            var response = await _productService.UpdateProduct(id, productViewModel);
            if (response.StatusCode==StatusCodeEnum.NotFound)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(response.Description);
        }
    }
}
