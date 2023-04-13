using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.HardCode.WebApi.Domain.Entity;
using WebApplication.HardCode.WebApi.Domain.Enum;
using WebApplication.HardCode.WebApi.Domain.Response;
using WebApplication.HardCode.WebApi.Domain.ViewModel;
using WebApplication.HardCode.WebAPI.DAL.Interfaces;
using WebApplication.HardCode.WebAPI.DAL.Repositories;
using WebApplication.HardCode.WebAPI.Service.Interfaces;

namespace WebApplication.HardCode.WebAPI.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepositories;
        private readonly ICategoryService _categoryService;
        private readonly ICategoryFieldService _categoryFieldService;

        public ProductService(IProductRepository productRepositories, ICategoryService categoryService,ICategoryFieldService categoryFieldService) 
        {
            _productRepositories=productRepositories;
            _categoryService=categoryService;
            _categoryFieldService=categoryFieldService;
        }
        public async Task<BaseResponse<bool>> AddProduct(ProductViewModel productViewModel)
        {
            try
            {
                var product = new Product()
                {
                    Name = productViewModel.Name,
                    ImageURL= productViewModel.ImageURL,
                    Description= productViewModel.Description,
                    Price = productViewModel.Price,
                    CategoryId= productViewModel.CategoryId,
                };
                await _productRepositories.Create(product);
                return new BaseResponse<bool>()
                {
                    Data= true,
                    Description="Новый товар создан",
                    StatusCode = WebApi.Domain.Enum.StatusCodeEnum.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data=false,
                    Description= $"{ex} + Не удалось создать товар",
                    StatusCode=StatusCodeEnum.InternalServerError,
                };
            }
        }

        public async Task<BaseResponse<bool>> DeleteProduct(int id)
        {
            try
            {
                var product = await _productRepositories.GetById(id);
                if(product==null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data=false,
                        Description="Товар не найден",
                        StatusCode=StatusCodeEnum.NotFound,
                    };
                }
                await _productRepositories.Delete(product);
                return new BaseResponse<bool>()
                {
                    Data= true,
                    Description="Товар удален",
                    StatusCode = WebApi.Domain.Enum.StatusCodeEnum.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data=false,
                    Description= $"{ex} + Не удалось удалить товар",
                    StatusCode=StatusCodeEnum.InternalServerError,
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<ProductViewModel>>> GetAllProducts()
        {
            try
            {
                var products = _productRepositories.GetAll().ToList();
                if (products==null)
                {
                    return new BaseResponse<IEnumerable<ProductViewModel>>()
                    {
                        Data=null,
                        Description="Товары не найден",
                        StatusCode=StatusCodeEnum.NotFound,
                    };
                }
                var productsViewModel = new List<ProductViewModel>();
                foreach (var item in products)
                {
                    var productViewModel = new ProductViewModel();
                    productViewModel.Name= item.Name;
                    productViewModel.Description= item.Description;
                    productViewModel.ImageURL= item.ImageURL;
                    productViewModel.Price = item.Price;
                    productViewModel.CategoryId = item.CategoryId;
                    productViewModel.CategoryViewModel = _categoryService.GetCategoryByIdAsync(item.CategoryId).Result.Data;
                    productViewModel.CategoryViewModel.Fields =_categoryFieldService.GetAllFieldsByCategoryIdAsync(productViewModel.CategoryId).Result.Data.ToList();
                    
                    productsViewModel.Add(productViewModel);
                }
                return new BaseResponse<IEnumerable<ProductViewModel>>()
                {
                    Data=productsViewModel,
                    Description="Все товары полученый",
                    StatusCode=StatusCodeEnum.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<ProductViewModel>>()
                {
                    Data=null,
                    Description= $"{ex} + Не удалось получить товары",
                    StatusCode=StatusCodeEnum.InternalServerError,
                };
            }
        }

        public async Task<BaseResponse<ProductViewModel>> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _productRepositories.GetById(id);
                if(product==null)
                {
                    return new BaseResponse<ProductViewModel>()
                    {
                        Data=null,
                        Description="Товар не найден",
                        StatusCode=StatusCodeEnum.NotFound,
                    };
                }
                var productViewModel = new ProductViewModel();
                productViewModel.Name= product.Name;
                productViewModel.Description= product.Description;
                productViewModel.ImageURL= product.ImageURL;
                productViewModel.Price = product.Price;
                productViewModel.CategoryId = product.CategoryId;
                productViewModel.CategoryViewModel = _categoryService.GetCategoryByIdAsync(product.CategoryId).Result.Data;
                productViewModel.CategoryViewModel.Fields =_categoryFieldService.GetAllFieldsByCategoryIdAsync(productViewModel.CategoryId).Result.Data.ToList();
                return new BaseResponse<ProductViewModel>()
                {
                    Data= productViewModel,
                    Description="Товар получен",
                    StatusCode = WebApi.Domain.Enum.StatusCodeEnum.OK
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductViewModel>()
                {
                    Data=null,
                    Description= $"{ex} + Не удалось получить товар",
                    StatusCode=StatusCodeEnum.InternalServerError,
                };
            }
        }

        public async Task<BaseResponse<bool>> UpdateProduct(int id, ProductViewModel productViewModel)
        {
            try
            {
                var product = await _productRepositories.GetById(id);
                if (product == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data=false,
                        Description="Товар не найден",
                        StatusCode=StatusCodeEnum.NotFound,
                    };
                }
                product.Name= productViewModel.Name;
                product.Description= productViewModel.Description;
                product.Price= productViewModel.Price;
                product.ImageURL= productViewModel.ImageURL;
                product.CategoryId= productViewModel.CategoryId;

                await _productRepositories.Update(product);
                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = "Обновление Товара произошло",
                    StatusCode=StatusCodeEnum.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data=false,
                    Description= $"{ex} + Не удалось обновить товар",
                    StatusCode=StatusCodeEnum.InternalServerError,
                };
            }
        }
    }
}
