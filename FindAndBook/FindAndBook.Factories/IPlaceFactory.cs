using System;
using FindAndBook.Models;

namespace FindAndBook.Factories
{
    public interface IPlaceFactory
    {
        Place CreatePlace(string name, string contact, string weekendHours,
            string weekdaayHours, string details, int? averageBill, User manager);
    }
}
