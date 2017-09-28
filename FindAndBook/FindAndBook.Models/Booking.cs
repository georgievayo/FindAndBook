using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindAndBook.Models
{
    public partial class Booking
    {
        public Guid Id { get; set; }

        public Guid? PlaceId { get; set; }

        public virtual Place Place { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime DateTime { get; set; }

        public int NumberOfPeople { get; set; }

        public virtual ICollection<Table> Tables { get; set; }
    }
}
