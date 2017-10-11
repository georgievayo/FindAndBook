using System;
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


        public PlaceService(IRepository<Place> placeRepository, IUnitOfWork unitOfWork,
            IPlaceFactory placeFactory)
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

            this.placeRepository = placeRepository;
            this.unitOfWork = unitOfWork;
            this.placeFactory = placeFactory;
        }

        public Place CreatePlace(string name, string type, string contact, 
            string weekendHours, string weekdaayHours, string details, int? averageBill, 
            string managerId, Guid? addressId)
        {
            var newPlace = this.placeFactory.CreatePlace(name, type, contact, weekendHours,
                weekdaayHours, details, averageBill, managerId, addressId);

            this.placeRepository.Add(newPlace);
            this.unitOfWork.Commit();

            return newPlace;
        }

        public IQueryable<Place> GetAll()
        {
            return this.placeRepository
                .All
                .Include(p => p.Reviews)
                .Include(p => p.Address);
        }

        public IQueryable<Place> GetPlaceById(Guid id)
        {
            return this.placeRepository.All
                .Where(x => x.Id == id)
                .Include(x => x.Address);
        }

        public IQueryable<Place> GetPlaceWithReviews(Guid? id)
        {
            return this.placeRepository.All
                .Where(x => x.Id == id)
                .Include(x => x.Address)
                .Include(x => x.Reviews);
        }

        public IQueryable<Place> GetUserPlaces(string userId)
        {
            return this.placeRepository.All
                .Where(x => x.ManagerId == userId)
                .Include(x => x.Address)
                .Include(x => x.Bookings);
        }

        public IQueryable<Place> GetPlacesByCategory(string category)
        {
            return this.placeRepository.All
                .Where(x => x.Type == category)
                .Include(x => x.Address)
                .Include(x => x.Reviews);
        }

        public Place EditPlace(Guid? id, string contact, string description, 
            string photoUrl, string weekdayHours, string weekendHours, int? averageBill)
        {
            var place = this.placeRepository.GetById(id);
            if (place != null)
            {
                place.Contact = contact;
                place.Details = description;
                place.PhotoUrl = photoUrl;
                place.WeekdayHours = weekdayHours;
                place.WeekendHours = weekendHours;
                place.AverageBill = averageBill;
                this.placeRepository.Update(place);
                this.unitOfWork.Commit();
            }

            return place;
        }

        public IQueryable<Place> FindInName(string category, string pattern)
        {
            return this.placeRepository
                .All
                .Where(x => x.Name.ToLower().Contains(pattern.ToLower()) && x.Type == category);
        }

        public IQueryable<Place> FindInDescription(string category, string pattern)
        {
            return this.placeRepository
                .All
                .Where(x => x.Details.ToLower().Contains(pattern.ToLower()) && x.Type == category);
        }

        public IQueryable<Place> FindInAddress(string category, string pattern)
        {
            var lowerPattern = pattern.ToLower();
            return this.placeRepository
                .All
                .Where(x => (x.Address.Country.ToLower().Contains(lowerPattern) || 
                x.Address.City.ToLower().Contains(lowerPattern) || 
                x.Address.Area.ToLower().Contains(lowerPattern) ||
                x.Address.Street.ToLower().Contains(lowerPattern)) && x.Type == category);
        }

        public IQueryable<Place> FindInBill(string category, string pattern)
        {
            return this.placeRepository
                .All
                .Where(x => x.AverageBill == int.Parse(pattern) && x.Type == category);
        }

        public void DeletePlace(Guid? id)
        {
            var place = this.placeRepository.GetById(id);
            this.placeRepository.Delete(place);
            this.unitOfWork.Commit();
        }
    }
}
