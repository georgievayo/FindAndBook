using System;
using System.Collections.Generic;

namespace FindAndBook.Models
{
    public partial class Booking
    {
        public Guid Id { get; set; }

        public Guid PlaceId { get; set; }

        public virtual Place Place { get; set; }

        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public int NumberOfPeople { get; set; }
    }
}
