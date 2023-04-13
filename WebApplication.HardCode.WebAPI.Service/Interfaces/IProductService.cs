using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.HardCode.WebApi.Domain.Response;
using WebApplication.HardCode.WebApi.Domain.ViewModel;

namespace WebApplication.HardCode.WebAPI.Service.Interfaces
{
    public interface IProductService
    {
        Task<BaseResponse<IEnumerable<ProductViewModel>>> GetAllProducts();
        Task<BaseResponse<ProductViewModel>> GetProductByIdAsync(int id);

        Task<BaseResponse<bool>> AddProduct(ProductViewModel productViewModel);
        Task<BaseResponse<bool>> UpdateProduct(int id, ProductViewModel productViewModel);

        Task<BaseResponse<bool>> DeleteProduct(int id);
    }
}
