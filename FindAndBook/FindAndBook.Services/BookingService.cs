using System;
using System.Data.Entity;
using System.Linq;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;

namespace FindAndBook.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Booking> bookingRepository;

        private readonly IUnitOfWork unitOfWork;

        private readonly IBookingFactory factory;

        public BookingService(IRepository<Booking> bookingRepository, IUnitOfWork unitOfWork, IBookingFactory factory)
        {
            if (bookingRepository == null)
            {
                throw new ArgumentNullException(nameof(bookingRepository));
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            this.bookingRepository = bookingRepository;
            this.unitOfWork = unitOfWork;
            this.factory = factory;
        }

        public IQueryable<Booking> GetBookingsOfPlace(Guid placeId)
        {
            return this.bookingRepository
                .All
                .Where(x => x.PlaceId == placeId);
        }

        public IQueryable<Booking> FindAllOn(DateTime dateTime, Guid? placeId)
        {
            return this.bookingRepository
                .All
                .Where(x => x.DateTime == dateTime && x.PlaceId == placeId)
                .Include(x => x.Tables);
        }

        public Booking GetById(Guid? id)
        {
            return this.bookingRepository.GetById(id);
        }

        public Booking CreateBooking(Guid? placeId, string userId, DateTime dateTime, int people)
        {
            var booking = this.factory.CreateBooking(placeId, userId, dateTime, people);
            this.bookingRepository.Add(booking);
            this.unitOfWork.Commit();
            return booking;
        }

        public void RemoveBooking(Guid? id)
        {
            var booking = this.bookingRepository.GetById(id);
            this.bookingRepository.Delete(booking);
            this.unitOfWork.Commit();
        }

        public void DeleteAll(Guid? placeId)
        {
            var bookings = this.bookingRepository
                .All
                .Where(x => x.PlaceId == placeId)
                .ToList();

            foreach (var booking in bookings)
            {
                this.bookingRepository.Delete(booking);
                this.unitOfWork.Commit();
            }
        }
    }
}
