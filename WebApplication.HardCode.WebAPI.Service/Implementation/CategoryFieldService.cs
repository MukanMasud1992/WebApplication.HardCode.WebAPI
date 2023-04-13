using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.HardCode.WebApi.Domain.Entity;
using WebApplication.HardCode.WebApi.Domain.Enum;
using WebApplication.HardCode.WebApi.Domain.Response;
using WebApplication.HardCode.WebApi.Domain.ViewModel;
using WebApplication.HardCode.WebAPI.DAL.Interfaces;
using WebApplication.HardCode.WebAPI.Service.Interfaces;

namespace WebApplication.HardCode.WebAPI.Service.Implementation
{
    public class CategoryFieldService : ICategoryFieldService
    {
        private readonly ICategoryFieldRepository _categoryFieldRepository;

        public CategoryFieldService(ICategoryFieldRepository categoryFieldRepository)
        {
            _categoryFieldRepository=categoryFieldRepository;
        }
        public async Task<IBaseResponse<bool>> CreateFieldAsync(CategoryFieldViewModel categoryViewModel)
        {
            try
            {
                var categoryField = new CategoryField()
                {
                    Name= categoryViewModel.Name,
                    Description = categoryViewModel.Description,
                    CategoryId = categoryViewModel.CategoryId,
                };
                await _categoryFieldRepository.Create(categoryField);
                return new BaseResponse<bool>()
                {
                    Data=true,
                    Description="Характеристика категории добавлена",
                    StatusCode=StatusCodeEnum.OK,
                };  

            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data=false,
                    Description= $"{ex} + Не удалось добавить характеристику категории",
                    StatusCode=StatusCodeEnum.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteFieldAsync(int id)
        {
            try
            {
                var categoryField = await _categoryFieldRepository.GetById(id);
                if (categoryField == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data=false,
                        Description="Характеристика категории не найдена",
                        StatusCode=StatusCodeEnum.NotFound,
                    };
                }
                await _categoryFieldRepository.Delete(categoryField);
                return new BaseResponse<bool>()
                {
                    Data=true,
                    Description="Характеристика категории удалена",
                    StatusCode=StatusCodeEnum.OK,
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data=false,
                    Description= $"{ex} + Не удалось удалить характеристику категории",
                    StatusCode=StatusCodeEnum.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<CategoryFieldViewModel>>> GetAllFieldsByCategoryIdAsync(int categoryId)
        {
            try
            {
                var categoryFields = await _categoryFieldRepository.GetAll();
                if (categoryFields==null)
                {
                    return new BaseResponse<IEnumerable<CategoryFieldViewModel>>()
                    {
                        Description="Найдено ноль элементов",
                        StatusCode=StatusCodeEnum.NotFound,
                        Data=null,
                    };
                }
            
                var categoryFieldsViewModel = categoryFields
                    .Where(cf=>cf.CategoryId==categoryId)
                    .Select(cf => new CategoryFieldViewModel()
                    {
                        Name= cf.Name,
                        Description=cf.Description,
                        CategoryId=cf.CategoryId,
                    }).ToList();
                return new BaseResponse<IEnumerable<CategoryFieldViewModel>>()
                {
                    Description="Найденные элементы",
                    StatusCode=StatusCodeEnum.OK,
                    Data=categoryFieldsViewModel,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<CategoryFieldViewModel>>()
                {
                    Data=null,
                    Description=$"GetAllFieldsByCategoryIdAsync + {ex.Message}",
                    StatusCode=StatusCodeEnum.InternalServerError
                };
            }
           

        }

        public async Task<IBaseResponse<CategoryFieldViewModel>> GetFieldByIdAsync(int id)
        {
            try
            {
                var categoryField = await _categoryFieldRepository.GetById(id);
                if (categoryField==null)
                {
                    return new BaseResponse<CategoryFieldViewModel>()
                    {
                        Description="Найдено ноль элементов",
                        StatusCode=StatusCodeEnum.NotFound,
                        Data=null,
                    };
                }
                var categoryFieldViewModel = new CategoryFieldViewModel()
                {
                    Name= categoryField.Name,
                    Description= categoryField.Description,
                    CategoryId = categoryField.CategoryId,
                };
                return new BaseResponse<CategoryFieldViewModel>()
                {
                    Description="Найденные элементы",
                    StatusCode=StatusCodeEnum.OK,
                    Data=categoryFieldViewModel,
                };

            }
            catch (Exception ex)
            {

                return new BaseResponse<CategoryFieldViewModel>()
                {
                    Data=null,
                    Description=$"GetById + {ex.Message}",
                    StatusCode=StatusCodeEnum.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> UpdateFieldAsync(int id, CategoryFieldViewModel categoryViewModel)
        {
            try
            {
                var categoryField = await _categoryFieldRepository.GetById(id);
                if (categoryField == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data=false,
                        Description="Характеристика категории не найдена",
                        StatusCode=StatusCodeEnum.OK,
                    };
                }
                categoryField.Name = categoryViewModel.Name;
                categoryField.Description = categoryViewModel.Description;
                categoryField.CategoryId = categoryViewModel.CategoryId;
                await _categoryFieldRepository.Update(categoryField);
                return new BaseResponse<bool>()
                {
                    Data=true,
                    Description="Характеристика найдена и обновлен",
                    StatusCode=StatusCodeEnum.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data=false,
                    Description= $"{ex} + Не удалось обновить характеристику категории",
                    StatusCode=StatusCodeEnum.InternalServerError,
                };
            }
        }
    }
}
