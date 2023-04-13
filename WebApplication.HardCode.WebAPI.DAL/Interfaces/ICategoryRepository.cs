using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.HardCode.WebApi.Domain.Entity;

namespace WebApplication.HardCode.WebAPI.DAL.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetCategoryById(int id);
        Task<bool> CreateCategory(Category category);

        Task<bool> UpdateCategory(Category category);
        Task<bool> DeleteCategory(int id);
        Task<IEnumerable<CategoryField>> GetFieldsByGategoryId(int id);
        Task<CategoryField> AddField(int categoryId, CategoryField categoryField);
    }
}
