using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Models
{
    public class OrderModel
    {
        public OrderModel()
        {
            OrderDetails = new HashSet<OrderDetailModel>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }

        public virtual UserModel Customer { get; set; }
        public virtual ICollection<OrderDetailModel> OrderDetails { get; set; }
    }
}
