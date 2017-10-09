using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindAndBook.Models
{
    public partial class Table
    {
        public Table()
        {
            this.Bookings = new HashSet<BookedTables>();
        }

        public Table(Guid? placeId, int numberOfPeople, int numberOfTables)
            : this()
        {
            this.PlaceId = placeId;
            this.NumberOfPeople = numberOfPeople;
            this.NumberOfTables = numberOfTables;
        }

        public Guid Id { get; set; }

        public Guid? PlaceId { get; set; }

        public virtual Place Place { get; set; }

        public int NumberOfPeople { get; set; }

        public int NumberOfTables { get; set; }

        public virtual ICollection<BookedTables> Bookings { get; set; }
    }
}
