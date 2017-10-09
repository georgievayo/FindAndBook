using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FindAndBook.Models
{
    public class User : IdentityUser, IUser
    {
        public User()
        {
            this.Bookings = new HashSet<Booking>();
            this.Reviews = new HashSet<Review>();
            this.Places = new HashSet<Place>();
        }

        public User(string username, string email, string firstName, string lastName, string phoneNumber)
            :this()
        {
            this.UserName = username;
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Place> Places { get; set; }
    }
}
