using System;
using System.Collections.Generic;
using FindAndBook.Models;
using System.Linq;

namespace FindAndBook.Services.Contracts
{
    public interface IPlaceService
    {
        Place CreatePlace(string name, string contact,
            string weekendHours, string weekdaayHours, string details, int? averageBill, string userId, Address address);

        IQueryable<Place> GetAll();

        IQueryable<Place> GetPlaceById(Guid id);
    }
}
