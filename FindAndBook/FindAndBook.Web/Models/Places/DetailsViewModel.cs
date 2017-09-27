using System;
using System.Collections.Generic;
using FindAndBook.Models;

namespace FindAndBook.Web.Models.Places
{
    public class DetailsViewModel
    {
        public DetailsViewModel(Place place, Address address)
        {
            this.Name = place.Name;
            this.Address = address;
            this.PhotoUrl = place.PhotoUrl;
            this.Contact = place.Contact;
            this.Details = place.Details;
            this.WeekdayHours = place.WeekdayHours;
            this.WeekendHours = place.WeekendHours;
            this.AverageBill = place.AverageBill;
            this.Consumables = place.Consumables;
            this.Reviews = place.Reviews;
        }

        public string Name { get; set; }

        public Address Address { get; set; }

        public string PhotoUrl { get; set; }

        public string Contact { get; set; }

        public string WeekendHours { get; set; }

        public string WeekdayHours { get; set; }

        public string Details { get; set; }

        public int? AverageBill { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<Consumable> Consumables { get; set; }
    }
}