using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.HardCode.WebApi.Domain.Response;
using WebApplication.HardCode.WebApi.Domain.ViewModel;

namespace WebApplication.HardCode.WebAPI.Service.Interfaces
{
    public interface ICategoryFieldService
    {
        Task<IBaseResponse<IEnumerable<CategoryFieldViewModel>>> GetAllFieldsByCategoryIdAsync(int categoryId);
        Task<IBaseResponse<CategoryFieldViewModel>> GetFieldByIdAsync(int id);
        Task<IBaseResponse<bool> >CreateFieldAsync(CategoryFieldViewModel CategoryFieldViewModel);
        Task<IBaseResponse<bool>> UpdateFieldAsync(int id,CategoryFieldViewModel CategoryFieldViewModel);
        Task<IBaseResponse<bool>> DeleteFieldAsync(int id);
    }
}
