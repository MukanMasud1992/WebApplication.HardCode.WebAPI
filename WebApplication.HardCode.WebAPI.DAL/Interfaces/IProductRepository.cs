using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.HardCode.WebApi.Domain.Entity;

namespace WebApplication.HardCode.WebAPI.DAL.Interfaces
{
    public interface IProductRepository
    {
        Task<bool> Create(Product entity);
        Task<bool> Delete(Product entity);
        IQueryable<Product> GetAll();
        Task<Product> GetById(int id);
        Task<bool> Update(Product entity);
    }
}
