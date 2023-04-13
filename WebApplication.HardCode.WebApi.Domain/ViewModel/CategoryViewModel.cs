using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.HardCode.WebApi.Domain.Entity;

namespace WebApplication.HardCode.WebApi.Domain.ViewModel
{
    public class CategoryViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<CategoryFieldViewModel> Fields { get; set; }
    }
}
