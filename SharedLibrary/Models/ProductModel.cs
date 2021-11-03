using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Models
{
    public class ProductModel
    {
        public ProductModel()
        {
            OrderDetails = new HashSet<OrderDetailModel>();
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public int QuantityPerUnit { get; set; }
        public int UnitsInStock { get; set; }

        public virtual ICollection<OrderDetailModel> OrderDetails { get; set; }
    }
}
