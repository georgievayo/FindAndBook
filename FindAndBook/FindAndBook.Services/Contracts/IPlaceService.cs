using System;
using System.Collections.Generic;
using FindAndBook.Models;

namespace FindAndBook.Services.Contracts
{
    public interface IPlaceService
    {
        Place CreatePlace(string name, string contact,
            string weekendHours, string weekdaayHours, string details, int? averageBill, string userId);

        ICollection<Place> GetAll();

        Place GetPlaceById(Guid id);
    }
}
