using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindAndBook.Models
{
    public partial class Address
    {
        public Address()
        {
            
        }
        public Address(string country, string city, string area, string street, int number)
        {
            this.Country = country;
            this.City = city;
            this.Area = area;
            this.Street = street;
            this.Number = number;
        }

        public Guid Id { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Area { get; set; }

        public string Street { get; set; }

        public int Number { get; set; }
    }
}
