using System;
using System.Linq;
using FindAndBook.Models;

namespace FindAndBook.Web.Models.Places
{
    public class PlaceShortViewModel
    {
        public PlaceShortViewModel(Place place, Address address)
        {
            this.Id = place.Id;
            this.Name = place.Name;
            this.PhotoUrl = place.PhotoUrl;
            this.Address = address;
            this.Contact = place.Contact;
            this.AverageBill = place.AverageBill;
            this.ReviewsCount = place.Reviews.Count;
            this.Rating = (double)place.Reviews.Sum(r => r.Rating) / place.Reviews.Count;
        }

        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string PhotoUrl { get; set; }

        public Address Address { get; set; }

        public string Contact { get; set; }

        public int? AverageBill { get; set; }

        public int ReviewsCount { get; set; }

        public double Rating { get; set; }
    }
}