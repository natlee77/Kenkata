using System;
using System.Collections.Generic;

#nullable disable

namespace KenkataWebApi.Entities
{
    public partial class ProductCategory
    {
        public int? ProductId { get; set; }
        public int? CategoriesId { get; set; }

        public virtual Category Categories { get; set; }
        public virtual Product Product { get; set; }
    }
}
