using System;
using System.Collections.Generic;

namespace FindAndBook.Models
{
    public partial class Place
    {
        public Place()
        {
            
        }
        public Place(string name, string contact, string weekendHours,
            string weekdaayHours, string details, int? averageBill, User manager)
        {
            this.Name = name;
            this.PhotoUrl = "http://basera-dfw.com/wp-content/uploads/2016/03/restaurant.jpeg";
            this.Contact = contact;
            this.WeekdayHours = weekdaayHours;
            this.WeekendHours = weekendHours;
            this.Details = details;
            this.AverageBill = averageBill;
            this.Manager = manager;
        }

        public Guid Id { get; set; }

        public string ManagerId { get; set; }

        public virtual User Manager { get; set; }

        public string Name { get; set; }

        public string PhotoUrl { get; set; }

        public string Contact { get; set; }

        public string WeekendHours { get; set; }

        public string WeekdayHours { get; set; }

        public string Details { get; set; }

        public int? AverageBill { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual ICollection<Consumable> Consumables { get; set; }


    }
}
