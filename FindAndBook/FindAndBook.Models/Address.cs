using System;
using System.Collections.Generic;

namespace FindAndBook.Models
{
    public partial class Address
    {
        public Guid PlaceId { get; set; }
        public virtual Place Place { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
    }
}
