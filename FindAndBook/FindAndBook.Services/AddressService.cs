using System;
using System.Linq;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;

namespace FindAndBook.Services
{
    public class AddressService : IAddressService
    {
        private IRepository<Address> addressRepository;

        private IUnitOfWork unitOfWork;

        private IAddressFactory addressFactory;

        public AddressService(IRepository<Address> addressRepository, IUnitOfWork unitOfWork,
            IAddressFactory addressFactory)
        {
            if (addressRepository == null)
            {
                throw new ArgumentNullException(nameof(addressRepository));
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            if (addressFactory == null)
            {
                throw new ArgumentNullException(nameof(addressFactory));
            }

            this.addressRepository = addressRepository;
            this.unitOfWork = unitOfWork;
            this.addressFactory = addressFactory;
        }

        public Address CreateAddress(Guid? placeId, string country, string city, string area, string street, int number)
        {
            var address = this.addressFactory.CreateAddress(placeId, country, city, area, street, number);
            this.addressRepository.Add(address);
            this.unitOfWork.Commit();

            return address;
        }

        public Address GetAddressByPlaceId(Guid placeId)
        {
            return this.addressRepository.All
                .ToList()
                .FirstOrDefault(p => p.PlaceId == placeId);
        }
    }
}
