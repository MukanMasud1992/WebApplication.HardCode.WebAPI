using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.HardCode.WebApi.Domain.Entity;

namespace WebApplication.HardCode.WebAPI.DAL.Interfaces
{
    public interface ICategoryFieldRepository
    {
        Task<IEnumerable<CategoryField>> GetAll();
        Task<CategoryField> GetById(int id);
        Task<IEnumerable<CategoryField>> GetAllByCategoryId(int id);
        Task<bool> Create(CategoryField categoryField);
        Task<bool> Update(CategoryField categoryField);
        Task<bool> Delete(CategoryField categoryField);

    }
}
