using System;
using System.Collections.Generic;
using System.Linq;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;

namespace FindAndBook.Services
{
    public class PlaceService : IPlaceService
    {
        private IRepository<Place> placeRepository;

        private IUnitOfWork unitOfWork;

        private IPlaceFactory placeFactory;

        private IUserService userService;

        public PlaceService(IRepository<Place> placeRepository, IUnitOfWork unitOfWork,
            IPlaceFactory placeFactory, IUserService userService)
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
        }

        public Place CreatePlace(string name, string contact, 
            string weekendHours, string weekdaayHours, string details, int? averageBill, string userId)
        {
            var manager = this.userService.GetUserById(userId);

            var newPlace = this.placeFactory.CreatePlace(name, contact, weekendHours, weekdaayHours, details,
                averageBill, manager);

            this.placeRepository.Add(newPlace);
            this.unitOfWork.Commit();

            return newPlace;
        }

        public ICollection<Place> GetAll()
        {
            var result = this.placeRepository.All.ToList();

            return result;
        }

    }
}
