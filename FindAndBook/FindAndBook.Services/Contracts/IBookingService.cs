using System;
using System.Linq;
using FindAndBook.Models;

namespace FindAndBook.Services.Contracts
{
    public interface IBookingService
    {
        IQueryable<Booking> GetBookingsOfPlace(Guid placeId);

        IQueryable<Booking> FindAllOn(DateTime dateTime, Guid? placeId);

        Booking GetById(Guid? id);

        Booking CreateBooking(Guid? placeId, string userId, DateTime dateTime);
    }
}
