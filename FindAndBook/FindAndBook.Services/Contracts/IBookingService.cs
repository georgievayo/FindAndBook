using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Models;

namespace FindAndBook.Services.Contracts
{
    public interface IBookingService
    {
        IQueryable<Booking> GetBookingsOfPlace(Guid placeId);
    }
}
