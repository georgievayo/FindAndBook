using System;
using System.Collections.Generic;

namespace FindAndBook.Data.Models
{
    public partial class Review
    {
        public string PlaceId { get; set; }
        public string UserId { get; set; }
        public string Review1 { get; set; }
        public int Rating { get; set; }
    }
}
