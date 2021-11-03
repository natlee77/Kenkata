using System;
using System.Collections.Generic;

#nullable disable

namespace KenkataWebApi.Entities
{
    public partial class Review
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
