using System;
using System.Collections.Generic;

namespace FindAndBook.Models
{
    public partial class Order
    {
        public Guid Id { get; set; }

        public Guid? BookingId { get; set; }

        public virtual Booking Booking { get; set; }

        public int Quantity { get; set; }

        public virtual ICollection<OrderedConsumable> Consumables { get; set; }
    }
}
 