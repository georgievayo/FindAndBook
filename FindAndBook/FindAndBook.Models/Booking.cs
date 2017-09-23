using System;
using System.Collections.Generic;

namespace FindAndBook.Models
{
    public partial class Booking
    {
        public string Id { get; set; }
        public string PlaceId { get; set; }
        public string UserId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int NumberOfPeople { get; set; }
    }
}
