using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Models
{
    public class OrderDetailModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        //public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        //public decimal Total => (decimal)(Price * Quantity);

        //public virtual OrderModel Order { get; set; }
        //public virtual ProductModel Product { get; set; }
    }
}
