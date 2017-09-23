using System;
using System.Collections.Generic;

namespace FindAndBook.Models
{
    public partial class Menu
    {
        public string PlaceId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Ingredients { get; set; }
    }
}
