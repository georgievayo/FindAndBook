using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindAndBook.Web.Models.Home
{
    public class HomeViewModel
    {
        public HomeViewModel(bool isAuthenticated)
        {
            this.IsAuthenticated = isAuthenticated;
        }

        public bool IsAuthenticated { get; set; }
    }
}