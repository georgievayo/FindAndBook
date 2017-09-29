using System;
using System.Linq;
using FindAndBook.Data.Contracts;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;

namespace FindAndBook.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Booking> placeRepository;

        private readonly IUnitOfWork unitOfWork;

        public BookingService(IRepository<Booking> placeRepository, IUnitOfWork unitOfWork)
        {
            if (placeRepository == null)
            {
                throw new ArgumentNullException(nameof(placeRepository));
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            this.placeRepository = placeRepository;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Booking> GetBookingsOfPlace(Guid placeId)
        {
            return this.placeRepository
                .All
                .Where(x => x.PlaceId == placeId);
        }
    }
}
