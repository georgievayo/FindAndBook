using System;
using FindAndBook.Models;

namespace FindAndBook.Factories
{
    public interface IPlaceFactory
    {
        Place CreatePlace(string name, string type, string contact, string weekendHours,
            string weekdaayHours, string details, int? averageBill, User manager, Address address);
    }
}
