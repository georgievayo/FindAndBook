using System;
using System.Linq;
using FindAndBook.Data.Contracts;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;

namespace FindAndBook.Services
{
    public class ConsumableService : IConsumableService
    {
        private readonly IRepository<Consumable> consumableRepository;
        private readonly IUnitOfWork unitOfWork;

        public ConsumableService(IRepository<Consumable> consumableRepository, IUnitOfWork unitOfWork)
        {
            if (consumableRepository == null)
            {
                throw new ArgumentNullException(nameof(consumableRepository));
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            this.consumableRepository = consumableRepository;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Consumable> GetAllConsumablesOf(Guid? placeId)
        {
            return this.consumableRepository.All.Where(x => x.PlaceId == placeId);
        }

        public Consumable GetByName(string name)
        {
            return this.consumableRepository.All.FirstOrDefault(x => x.Name == name);
        }

        public void AddBooking(Consumable consumable, Booking booking)
        {
            consumable.Bookings.Add(booking);
            this.unitOfWork.Commit();
        }
    }
}
