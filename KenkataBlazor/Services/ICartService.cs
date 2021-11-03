using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KenkataBlazor.Services
{
    public interface ICartService
    {
        event Action OnChange;
        Task AddToCart(ShoppingCartModel productModel);
        //Task<List<CartItem>> GetCartItems();
        Task DeleteItem(ShoppingCartModel item);
        Task<PlaceOrderResponseModel> PlaceOrder(List<ShoppingCartModel> cartList);
        void UpdateComponents();
    }
}
