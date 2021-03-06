using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindAndBook.Models
{
    public partial class Place
    {
        public Place()
        {
            this.Reviews = new HashSet<Review>();
            this.Consumables = new HashSet<Consumable>();
            this.Bookings = new HashSet<Booking>();
            this.Tables = new HashSet<Table>();
        }
        public Place(string name, string type, string contact, string weekendHours,
            string weekdaayHours, string details, int? averageBill, string managerId, Guid? addressId)
            : this()
        {
            this.Name = name;
            this.Type = type;
            this.PhotoUrl = "http://basera-dfw.com/wp-content/uploads/2016/03/restaurant.jpeg";
            this.Contact = contact;
            this.WeekdayHours = weekdaayHours;
            this.WeekendHours = weekendHours;
            this.Details = details;
            this.AverageBill = averageBill;
            this.ManagerId = managerId;
            this.AddressId = addressId;
        }

        public Guid Id { get; set; }

        public Guid? AddressId { get; set; }

        public Address Address { get; set; }

        public string ManagerId { get; set; }

        public virtual User Manager { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string PhotoUrl { get; set; }

        public string Contact { get; set; }

        public string WeekendHours { get; set; }

        public string WeekdayHours { get; set; }

        public string Details { get; set; }

        public int? AverageBill { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual ICollection<Consumable> Consumables { get; set; }

        public virtual ICollection<Table> Tables { get; set; }
    }
}
