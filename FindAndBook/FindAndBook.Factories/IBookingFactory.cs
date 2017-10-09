using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Models;

namespace FindAndBook.Factories
{
    public interface IBookingFactory
    {
        Booking CreateBooking(Guid? placeId, string userId, DateTime dateTime, int people);
    }
}
