using System;
using System.Collections.Generic;

namespace FindAndBook.Models
{
    public partial class Order
    {
        public Guid BookingId { get; set; }

        public virtual Booking Booking { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }
    }
}
 