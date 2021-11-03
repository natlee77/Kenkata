using Blazored.LocalStorage;
using Microsoft.Rest;
using Newtonsoft.Json;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KenkataBlazor.Services
{
    public class CartService : ICartService
    {
        // funkar ej att göra dep injection på IproductService och productservice
        private readonly ILocalStorageService _localStorage;
        public event Action OnChange;
        private readonly HttpClient _client;   

        public CartService(ILocalStorageService localStorage, HttpClient client)
        {
            _localStorage = localStorage;
            _client = client;
        }


        public async Task AddToCart(ShoppingCartModel shoppingCart)
        {
            var cart = await _localStorage.GetItemAsync<List<ShoppingCartModel>>("cart");
            if (cart == null)
            {
                cart = new List<ShoppingCartModel>();
            }
            var sameItem = cart.Find(c => c.ProductId == shoppingCart.ProductId);
            if (sameItem == null)
                cart.Add(shoppingCart);
            else
                sameItem.QuantityByUser += shoppingCart.QuantityByUser;
            
            await _localStorage.SetItemAsync("cart", cart);

            OnChange.Invoke();
        }


        public async Task DeleteItem(ShoppingCartModel item)
        {
            var cart = await _localStorage.GetItemAsync<List<ShoppingCartModel>>("cart");
            if (cart == null)
            {
                return;
            }

            var cartItem = cart.Find(x => x.ProductId == item.ProductId);
            cart.Remove(cartItem);

            await _localStorage.SetItemAsync("cart", cart);
            OnChange.Invoke();
        }




        public async Task<PlaceOrderResponseModel> PlaceOrder(List<ShoppingCartModel> cartList)
        {
            var Content = JsonConvert.SerializeObject(cartList);
            var buffer = System.Text.Encoding.UTF8.GetBytes(Content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _client.PostAsync("api/orders/create", byteContent);

            if (response.IsSuccessStatusCode)
                return new PlaceOrderResponseModel() { Message = "Your Order placement was successful!", Succeeded = true };
            else
                return new PlaceOrderResponseModel() { Message = "Your Order placement failed!", Succeeded = false };
        }

        public void UpdateComponents()
        {
            OnChange.Invoke();
        }



        //public async Task<List<CartItem>> GetCartItems()
        //{

        //    var result = new List<CartItem>();
        //    var cart = await _localStorage.GetItemAsync<List<ProductModel>>("cart");
        //    if (cart == null)
        //    {
        //        return result;
        //    }
        //    foreach (var item in cart)
        //    {
        //        var product = await _productService.GetProductAsync(item.Id); // try GetProductsAsync(item.Id)
        //        var cartItem = new CartItem
        //        {
        //            ProductId = product.Id,
        //            ProductTitle = product.Name,
        //            ProductPrice = product.price

        //        };

        //        return result;
        //    }

        //}
    }
}

