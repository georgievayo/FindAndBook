using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindAndBook.Models;

namespace FindAndBook.Web.Areas.Administration.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            
        }

        public UserViewModel(User user, bool isAdmin)
        {
            User = user;
            IsAdmin = isAdmin;
        }

        public User User { get; set; }

        public bool IsAdmin { get; set; }
    }
}