using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;

namespace FindAndBook.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly IRepository<Place> placeRepository;

        private readonly IUnitOfWork unitOfWork;

        private readonly IPlaceFactory placeFactory;

        private readonly IUserService userService;

        private readonly IAddressService addressService;

        public PlaceService(IRepository<Place> placeRepository, IUnitOfWork unitOfWork,
            IPlaceFactory placeFactory, IUserService userService, IAddressService addressService)
        {
            if (placeRepository == null)
            {
                throw new ArgumentNullException(nameof(placeRepository));
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            if (placeFactory == null)
            {
                throw new ArgumentNullException(nameof(placeFactory));
            }

            if (userService == null)
            {
                throw new ArgumentNullException(nameof(userService));
            }

            this.placeRepository = placeRepository;
            this.unitOfWork = unitOfWork;
            this.placeFactory = placeFactory;
            this.userService = userService;
            this.addressService = addressService;
        }

        public Place CreatePlace(string name, string contact, 
            string weekendHours, string weekdaayHours, string details, int? averageBill, string userId, Address address)
        {
            var manager = this.userService.GetUserById(userId);
            var newPlace = this.placeFactory.CreatePlace(name, contact, weekendHours, weekdaayHours, details,
                averageBill, manager, address);

            this.placeRepository.Add(newPlace);
            this.unitOfWork.Commit();

            return newPlace;
        }

        public IQueryable<Place> GetAll()
        {
            return this.placeRepository.All.Include(p => p.Reviews);
        }

        public IQueryable<Place> GetPlaceById(Guid id)
        {
            return this.placeRepository.All
                .Where(x => x.Id == id)
                .Include(x => x.Address);
        }

    }
}
