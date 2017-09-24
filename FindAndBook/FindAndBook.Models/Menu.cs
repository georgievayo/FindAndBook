using System;
using System.Collections.Generic;

namespace FindAndBook.Models
{
    public partial class Menu
    {
        public Guid PlaceId { get; set; }

        public virtual Place Place { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        public string Ingredients { get; set; }
    }
}
