using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApplication.HardCode.WebApi.Domain.Enum;
using WebApplication.HardCode.WebApi.Domain.ViewModel;
using WebApplication.HardCode.WebAPI.Service.Interfaces;

namespace WebApplication.HardCode.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService= categoryService;
        }

        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _categoryService.AddCategoryAsync(categoryViewModel);
            if (response.StatusCode==StatusCodeEnum.OK)
            {
                return Ok("Successfully created");
            }
            return StatusCode(500);
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _categoryService.GetAllCategories();
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
        [Route("GetCategoriseById/{id}")]
        public async Task<IActionResult> GetCategoriseById(int id)
        {
            var response = await _categoryService.GetCategoryByIdAsync(id);
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
        [Route("DeleteCategoryById/{id}")]
        public async Task<IActionResult> DeleteCategoryById(int id)
        {
            var response = await _categoryService.DeleteCategory(id);
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
        [Route("UpdateCategoryByyId/{id}")]
        public async Task<IActionResult> UpdateCategoryByyId(int id, [FromBody] CategoryViewModel categoryViewModel)
        {
        
            var response = await _categoryService.UpdateCategory(id, categoryViewModel);
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
