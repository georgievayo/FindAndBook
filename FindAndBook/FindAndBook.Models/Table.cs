using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindAndBook.Models
{
    public partial class Table
    {
        public Guid Id { get; set; }

        public Guid? PlaceId { get; set; }

        public virtual Place Place { get; set; }

        public int NumberOfPeople { get; set; }

        public int NumberOfTables { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
