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
    public class CategoryFeildController : Controller
    {
        private readonly ICategoryFieldService _categoryFieldService;

        public CategoryFeildController(ICategoryFieldService categoryFieldService)
        {
            _categoryFieldService=categoryFieldService;
        }

        [HttpPost]
        [Route("CreateCategoryField")]
        public async Task<IActionResult> CreateCategoryField([FromBody] CategoryFieldViewModel categoryFeildViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _categoryFieldService.CreateFieldAsync(categoryFeildViewModel);
            if (response.StatusCode==StatusCodeEnum.OK)
            {
                return Ok("Successfully created");
            }
            return StatusCode(500);
        }

        [HttpGet]
        [Route("GetAllCategoriesFieldsOfCategoryById/{id}")]
        public async Task<IActionResult> GetAllCategoriesFields(int id)
        {
            var response = await _categoryFieldService.GetAllFieldsByCategoryIdAsync(id);
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
        [Route("GetFieldById/{id}")]
        public async Task<IActionResult> GetFieldById(int id)
        {
            var response = await _categoryFieldService.GetFieldByIdAsync(id);
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
            var response = await _categoryFieldService.DeleteFieldAsync(id);
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
        public async Task<IActionResult> UpdateCategoryByyId(int id, [FromBody] CategoryFieldViewModel categoryFieldViewModel)
        {

            var response = await _categoryFieldService.UpdateFieldAsync(id, categoryFieldViewModel);
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
