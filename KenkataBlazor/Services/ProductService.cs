using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedLibrary.Models; 
using System.Net.Http;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Microsoft.JSInterop;

namespace KenkataBlazor.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient http;
        private readonly ILocalStorageService _localStorage;
        public event Action OnChange;
        public ProductService(HttpClient http, ILocalStorageService  localStorage)
        {
            this.http = http;
            this._localStorage = localStorage;
        }

        public ProductModel Product { get; set; }        
        public IEnumerable<ProductModel>  Products { get  ; set  ; }

        
        

        public async Task  GetProductsAsync() //funkar ==LoadProducts(i lection) anropas i Task function --await ProductService.GetProductsAsync(); 
        {
            Products = new List<ProductModel>();
            Products = await http.GetFromJsonAsync<IEnumerable<ProductModel>>("/api/Products");//updatera & hämta
        }

        //add to compare
        public async Task AddToCompare(CompareModel compareList)
        {
            var compare = await _localStorage.GetItemAsync<List<CompareModel>>("compare");
            if (compare == null)
            {
                compare = new List<CompareModel>();
            }
            var sameItem = compare.Find(c => c.Id == compareList.Id);
            if (sameItem == null)
                compare.Add(compareList);
            else { }
            await _localStorage.SetItemAsync("compare", compare);
            //OnChange.Invoke();
        }
        public async Task DeleteCompareItem(CompareModel item)
        {
            var compare = await _localStorage.GetItemAsync<List<ProductModel>>("compare");
            if (compare == null)
            {
                return;
            }

            var compareItem = compare.Find(x => x.Id == item.Id);
            compare.Remove(compareItem);

             await _localStorage.SetItemAsync("compare", compare);
            //await _localStorage.SetItemAsync(compareItem.Id.ToString() , compareItem);
            OnChange.Invoke();
        }
         
        //public async Task Delete()
        //{
        //    await JSRuntime.InvokeAsync<string>("localStorage.removeItem", "wish");
        //}
        //AddToWish
        public async Task AddToWish(DataModel wishList)
        {
            var wish = await _localStorage.GetItemAsync<List<DataModel>>("wish");
            if (wish == null)
            {
                wish = new List<DataModel>();
            }
            var sameItem = wish.Find(c => c.Id == wishList.Id);
            if (sameItem == null)
                wish.Add(wishList);
            else { }
            await _localStorage.SetItemAsync("wish", wish);
            //OnChange.Invoke();
        }
        public async Task DeleteWishItem(DataModel item)
        {
            var wish = await _localStorage.GetItemAsync<List<ProductModel>>("wish");
            if (wish == null)
            {
                return;
            }
            else { }
            var wishItem = wish.Find(x => x.Id == item.Id);
            wish.Remove(wishItem);

            await _localStorage.SetItemAsync("wish", wish);
            //await _localStorage.SetItemAsync(wishItem.Id.ToString(), wishItem);
            OnChange.Invoke();
        }

    }
}
