using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindAndBook.Models;

namespace FindAndBook.Web.Models.Places
{
    public class PlaceShortViewModel
    {
        public PlaceShortViewModel(Place place, Address address)
        {
            this.Name = place.Name;
            this.Address = address;
            this.Contact = place.Contact;
            this.AverageBill = place.AverageBill;
        }

        public string Name { get; set; }

        public Address Address { get; set; }

        public string Contact { get; set; }

        public int? AverageBill { get; set; }
    }
}