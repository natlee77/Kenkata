using System;
using System.Collections.Generic;

#nullable disable

namespace KenkataWebApi.Entities
{
    public partial class Category
    {
        public int Id { get; set; }
        public string CategoriesName { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
    }
}
