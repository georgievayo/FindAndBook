using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindAndBook.Web.Models.Places
{
    public class BookingViewModel
    {
        public BookingViewModel()
        {
            
        }
        public BookingViewModel(Guid? id)
        {
            PlaceId = id;
        }

        public Guid? PlaceId { get; set; }
        public DateTime DateTime { get; set; }
    }
}