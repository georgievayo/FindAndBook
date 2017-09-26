using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Models;

namespace FindAndBook.Factories
{
    public interface IAddressFactory
    {
        Address CreateAddress(Place place, string country, string city, string area, string street, int number);
    }
}
