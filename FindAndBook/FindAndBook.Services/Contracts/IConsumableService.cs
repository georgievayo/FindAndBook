using System;
using System.Linq;
using FindAndBook.Models;

namespace FindAndBook.Services.Contracts
{
    public interface IConsumableService
    {
        IQueryable<Consumable> GetAllConsumablesOf(Guid? placeId);

        Consumable GetByName(string name);

        void AddBooking(Consumable consumable, Booking booking);

        Consumable AddConsumable(Guid? placeId, string name, int quantity, 
            decimal? price, string type, string ingredients);
    }
}
