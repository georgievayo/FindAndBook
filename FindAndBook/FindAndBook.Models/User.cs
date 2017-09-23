using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FindAndBook.Models
{
    public class User : IdentityUser
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

        public ICollection<Booking> Bookings { get; set; }



    }
}
