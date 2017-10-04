using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FindAndBook.Web.Models.Places
{
    public class BookingFormViewModel
    {
        public BookingFormViewModel()
        {
            
        }
        public BookingFormViewModel(int two, int four, int six, Guid? placeId, DateTime dateTime)
        {
            TwoPeopleTablesCount = two;
            FourPeopleTablesCount = four;
            SixPeopleTablesCount = six;
        }

        public Guid? PlaceId { get; set; }

        public DateTime DateTime { get; set; }

        public int TwoPeopleTablesCount { get; set; }

        public int FourPeopleTablesCount { get; set; }

        public int SixPeopleTablesCount { get; set; }

        public int TwoPeopleInput { get; set; }

        public int FourPeopleInput { get; set; }

        public int SixPeopleInput { get; set; }
    }
}