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
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _dbContext;

        public ProductRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Create(Product entity)
        {
            _dbContext.Products.Add(entity);
            return Save().Result;
        }

        public async Task<bool> Delete(Product entity)
        {
            _dbContext.Products.Remove(entity);
            return Save().Result;
        }

        public IQueryable<Product> GetAll()
        {
           return _dbContext.Products.Include(p => p.Category);
        }

        public async Task<Product> GetById(int id)
        {
            return _dbContext.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
        }

        public async Task<bool> Update(Product entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return await Save();
        }
        private async Task<bool> Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved>0 ? true : false;
        }
    }
}
