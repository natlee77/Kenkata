using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace KenkataBlazor.Services
{
    public class CategoryService : ICategoryService
    {


        private readonly HttpClient http;
        public CategoryService(HttpClient http)
        {
            this.http = http;
        }
         
        public IEnumerable<CategoriesModel> Categories { get; set; }
        
        public async Task  LoadCategoties()   //funkar    anropas--await CategoryService.LoadCategoties();         
        {
            Categories = new List<CategoriesModel>();
            Categories = await http.GetFromJsonAsync<IEnumerable<CategoriesModel>>("/api/Categories");//updatera & hämta
           
                
        }

    }
}
