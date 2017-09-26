using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using FindAndBook.Models.Enumerations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FindAndBook.Models
{
    public class User : IdentityUser, IUser
    {
        public User()
        {
            this.Bookings = new HashSet<Booking>();
        }

        public User(string username, string email)
            :this()
        {
            this.UserName = username;
            this.Email = email;
        }


        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
