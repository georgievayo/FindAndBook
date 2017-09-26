using FindAndBook.Models;

namespace FindAndBook.Services.Contracts
{
    public interface IAddressService
    {
        Address CreateAddress(Place place, string country, string city, string area, string street, int number);
    }
}
