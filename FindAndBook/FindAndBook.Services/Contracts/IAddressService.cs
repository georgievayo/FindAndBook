using System;
using FindAndBook.Models;

namespace FindAndBook.Services.Contracts
{
    public interface IAddressService
    {
        Address CreateAddress(string country, string city, string area, string street, int number);

        //Address GetAddressByPlaceId(Guid placeId);
        Address EditAddress(Guid? placeId, string country, string city, string area, string street, int number);
    }
}
