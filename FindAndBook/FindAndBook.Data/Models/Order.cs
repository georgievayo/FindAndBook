using System;
using System.Collections.Generic;

namespace FindAndBook.Data.Models
{
    public partial class Order
    {
        public string BookingId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
