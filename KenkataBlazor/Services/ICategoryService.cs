using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KenkataBlazor.Services
{
    interface ICategoryService
    {
        public IEnumerable<CategoriesModel> Categories { get; set; }
        Task LoadCategoties();

    }
}
