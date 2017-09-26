using System;
using System.Collections.Generic;

namespace FindAndBook.Models
{
    public partial class Review
    {
        public Guid Id { get; set; }

        public Guid? PlaceId { get; set; }

        public virtual Place Place { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public string Message { get; set; }

        public int Rating { get; set; }
    }
}
