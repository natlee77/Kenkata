using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Models
{
    public class ShoppingCartModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int QuantityByUser { get; set; }
        public decimal SubTotal => Price * QuantityByUser;
        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; }
    }
}
