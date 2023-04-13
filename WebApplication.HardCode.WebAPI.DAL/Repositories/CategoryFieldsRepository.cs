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
    public class CategoryFieldsRepository : ICategoryFieldRepository
    {
        private readonly DataContext _dataContext;

        public CategoryFieldsRepository(DataContext dataContext) 
        {
            _dataContext=dataContext;
        }

        public async Task<bool> Create(CategoryField categoryField)
        {
            _dataContext.CategoryFields.Add(categoryField);
            return Save().Result;
        }

        public async Task<bool> Delete(CategoryField categoryField)
        {
            _dataContext.CategoryFields.Remove(categoryField);
            return Save().Result;
        }

        public async Task<IEnumerable<CategoryField>> GetAll()
        {
            return await _dataContext.CategoryFields.ToListAsync();
        }

        public async Task<IEnumerable<CategoryField>> GetAllByCategoryId(int id)
        {
           return await _dataContext.CategoryFields
                .Where(c => c.CategoryId == id).ToListAsync();
        }

        public async Task<CategoryField> GetById(int id)
        {
            return await _dataContext.CategoryFields.FindAsync(id);
        }

        public async Task<bool> Update(CategoryField categoryField)
        {
            _dataContext.CategoryFields.Update(categoryField);
            return Save().Result;
        }

        private async Task<bool> Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved>0 ? true : false;
        }
    }
}
