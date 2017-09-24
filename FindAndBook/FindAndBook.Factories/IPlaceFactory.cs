using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Models;

namespace FindAndBook.Factories
{
    public interface IPlaceFactory
    {
        Place CreatePlace(string name, string contact, string weekendHours, string weekdaayHours, string details, int? averageBill);
    }
}
