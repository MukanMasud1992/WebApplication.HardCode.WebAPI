using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.HardCode.WebApi.Domain.Entity;

namespace WebApplication.HardCode.WebAPI.DAL
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; } 

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryField> CategoryFields { get; set; }

    }
}
