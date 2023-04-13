using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.HardCode.WebApi.Domain.Entity;
using WebApplication.HardCode.WebApi.Domain.Enum;
using WebApplication.HardCode.WebApi.Domain.Response;
using WebApplication.HardCode.WebApi.Domain.ViewModel;
using WebApplication.HardCode.WebAPI.DAL.Interfaces;
using WebApplication.HardCode.WebAPI.Service.Interfaces;

namespace WebApplication.HardCode.WebAPI.Service.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryFieldRepository _categoryFieldRepository;

        public CategoryService(ICategoryRepository categoryRepository,ICategoryFieldRepository categoryFieldRepository) 
        {
            _categoryRepository=categoryRepository;
            _categoryFieldRepository=categoryFieldRepository;
        }
        public async Task<BaseResponse<bool>> AddCategoryAsync(CategoryViewModel categoryViewModel)
        {
            try
            {
                var category = new Category()
                {
                    Name= categoryViewModel.Name,
                    Description = categoryViewModel.Description,
                };
                await _categoryRepository.CreateCategory(category);
                return new BaseResponse<bool>()
                {
                    Data= true,
                    Description = "Создали новую категорию",
                    StatusCode = WebApi.Domain.Enum.StatusCodeEnum.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data=false,
                    Description= $"{ex} + Не удалось добавить категорию",
                    StatusCode=StatusCodeEnum.InternalServerError,
                };
            }
        }

        public async Task<BaseResponse<bool>> DeleteCategory(int id)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryById(id);
                if (category==null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data=false,
                        Description="Категория не найдена",
                        StatusCode=StatusCodeEnum.NotFound,
                    };
                }
                await _categoryRepository.DeleteCategory(id);
                return new BaseResponse<bool>()
                {
                    Data=true,
                    Description="Категория удалена",
                    StatusCode=StatusCodeEnum.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data=false,
                    Description= $"{ex} + Не удалось удалить категорию",
                    StatusCode=StatusCodeEnum.InternalServerError,
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<CategoryViewModel>>> GetAllCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetAll();
                if(categories==null)
                {
                    return new BaseResponse<IEnumerable<CategoryViewModel>>()
                    {
                        Data=null,
                        Description="Категория не найдена",
                        StatusCode=StatusCodeEnum.NotFound,
                    };
                }
                var categoriesViewModel = new List<CategoryViewModel>();
                foreach (var item in categories)
                {
                    var cvm = new CategoryViewModel();
                    cvm.Name = item.Name;
                    cvm.Description = item.Description;
                    cvm.Fields = new List<CategoryFieldViewModel>();
                    foreach (var field in item.Fields)
                    {
                        var categoriesFieldViewModel = new CategoryFieldViewModel()
                        {
                            Name = field.Name,
                            Description= field.Description,
                            CategoryId= field.CategoryId,
                        };
                        cvm.Fields.Add(categoriesFieldViewModel);
                    }
                    categoriesViewModel.Add(cvm);
                }
                return new BaseResponse<IEnumerable<CategoryViewModel>>()
                {
                    Data=categoriesViewModel,
                    Description = "Все категории полученый",
                    StatusCode=StatusCodeEnum.OK,
                };
               
                
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<CategoryViewModel>>()
                {
                    Data=null,
                    Description= $"{ex} + Не удалось получить категории",
                    StatusCode=StatusCodeEnum.InternalServerError,
                };
            }
        }

        public async Task<BaseResponse<CategoryViewModel>> GetCategoryByIdAsync(int id)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryById(id);
                if(category == null)
                {
                    return new BaseResponse<CategoryViewModel>()
                    {
                        Data=null,
                        Description="Категория не найдена",
                        StatusCode=StatusCodeEnum.NotFound,
                    };
                }
                var categoriesViewModel = new CategoryViewModel()
                {
                    Name = category.Name,
                    Description= category.Description,
                    Fields= category.Fields.Select(cf => new CategoryFieldViewModel()
                    {
                        Name = cf.Name,
                        Description= cf.Description,
                        CategoryId= cf.CategoryId,
                    }).ToList(),
                };
                return new BaseResponse<CategoryViewModel>()
                {
                    Data=categoriesViewModel,
                    Description = "Все категории полученый",
                    StatusCode=StatusCodeEnum.OK,
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<CategoryViewModel>()
                {
                    Data=null,
                    Description= $"{ex} + Не удалось получить категории",
                    StatusCode=StatusCodeEnum.InternalServerError,
                };
  
            }
        }

        public async Task<BaseResponse<bool>> UpdateCategory(int id, CategoryViewModel categoryViewModel)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryById(id);
                if (category == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data=false,
                        Description="Категория не найдена",
                        StatusCode=StatusCodeEnum.NotFound,
                    };
                }
                category.Name = categoryViewModel.Name;
                category.Description = categoryViewModel.Description;
                if(category.Fields.Count!=0) 
                {
                    category.Fields = categoryViewModel.Fields.Select(cf => new CategoryField()
                    {
                        Name= cf.Name,
                        Description= cf.Description,
                        CategoryId= cf.CategoryId,
                    }).ToList();
                }
                await _categoryRepository.UpdateCategory(category);
                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = "Обновление категории произошло",
                    StatusCode=StatusCodeEnum.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data=false,
                    Description= $"{ex} + Не удалось обновить категорию",
                    StatusCode=StatusCodeEnum.InternalServerError,
                };
            }
           
        }
    }
}
