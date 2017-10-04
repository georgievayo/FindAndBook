using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindAndBook.Models;

namespace FindAndBook.Web.Models.Bookings
{
    public class OrderFormViewModel
    {
        public OrderFormViewModel()
        {
            
        }

        public OrderFormViewModel(Guid? bookingId, ICollection<Consumable> placeMenu)
        {
            this.BookingId = bookingId;
            this.PlaceMenu = placeMenu;
        }

        public Guid? BookingId { get; set; }

        public ICollection<Consumable> PlaceMenu { get; set; }
    }
}