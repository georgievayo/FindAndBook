using System;
using System.Collections.Generic;

namespace FindAndBook.Models
{
    public partial class BookedTable
    {
        public Guid BookingId { get; set; }

        public virtual Booking Booking { get; set; }

        public Guid TableId { get; set; }

        public virtual Table Table { get; set; }
    }
}
