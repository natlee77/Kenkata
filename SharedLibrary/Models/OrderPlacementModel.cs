using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Models
{

    public class OrderPlacementModel
    {
        public int CustomerId { get; set; }
        public OrderProduct[] Products { get; set; }
    }

    public class OrderProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

}
