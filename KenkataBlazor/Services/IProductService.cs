using KenkataBlazor.Pages;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KenkataBlazor.Services
{
    interface IProductService
    {

        IEnumerable<ProductModel> Products { get; set; }  
        ProductModel Product { get; set; }


        Task GetProductsAsync();        
        Task AddToCompare(CompareModel compareList);
        Task DeleteCompareItem(CompareModel item);

        Task AddToWish(DataModel wishList);
        Task DeleteWishItem(DataModel item);
    }
}
