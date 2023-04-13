using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.HardCode.WebApi.Domain.Entity;
using WebApplication.HardCode.WebAPI.DAL.Interfaces;

namespace WebApplication.HardCode.WebAPI.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _dbcontext;

        public CategoryRepository(DataContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<CategoryField> AddField(int categoryId, CategoryField categoryField)
        {
            categoryField.CategoryId = categoryId;
            await _dbcontext.CategoryFields.AddAsync(categoryField);
            await _dbcontext.SaveChangesAsync();
            return categoryField;
        }

        public async Task<bool> CreateCategory(Category category)
        {
            await _dbcontext.Categories.AddAsync(category);
            return await Save();
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await GetCategoryById(id);
            if (category==null)
            {
                return false;
            }
            _dbcontext.Categories.Remove(category);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _dbcontext.Categories.Include(c => c.Fields).ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _dbcontext.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<CategoryField>> GetFieldsByGategoryId(int id)
        {
            return await _dbcontext.CategoryFields.Where(cf=>cf.CategoryId==id).ToListAsync();
        }

        public async Task<bool> UpdateCategory(Category category)
        {
           _dbcontext.Entry(category).State = EntityState.Modified;
            return await Save();

        }
        private async Task<bool> Save()
        {
            var saved = _dbcontext.SaveChanges();
            return saved>0 ? true : false;
        }
    }
}
