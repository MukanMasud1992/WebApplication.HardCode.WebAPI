using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.HardCode.WebApi.Domain.Response;
using WebApplication.HardCode.WebApi.Domain.ViewModel;

namespace WebApplication.HardCode.WebAPI.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<BaseResponse<IEnumerable<CategoryViewModel>>> GetAllCategories();
        Task<BaseResponse<CategoryViewModel>> GetCategoryByIdAsync(int id);

        Task<BaseResponse<bool>> AddCategoryAsync(CategoryViewModel categoryViewModel);
        Task<BaseResponse<bool>> UpdateCategory(int id, CategoryViewModel categoryViewModel);

        Task<BaseResponse<bool>> DeleteCategory(int id);
    }
}
