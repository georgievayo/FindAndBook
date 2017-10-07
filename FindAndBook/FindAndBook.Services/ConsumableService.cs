using System;
using System.Linq;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;

namespace FindAndBook.Services
{
    public class ConsumableService : IConsumableService
    {
        private readonly IRepository<Consumable> consumableRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IConsumableFactory factory;

        public ConsumableService(IRepository<Consumable> consumableRepository, IUnitOfWork unitOfWork, IConsumableFactory factory)
        {
            if (consumableRepository == null)
            {
                throw new ArgumentNullException(nameof(consumableRepository));
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            this.consumableRepository = consumableRepository;
            this.unitOfWork = unitOfWork;
            this.factory = factory;
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

        public Consumable AddConsumable(Guid? placeId, string name, int quantity, decimal? price, string type, string ingredients)
        {
            var consumable = this.factory.CreateConsumable(placeId, name, quantity, price, type, ingredients);
            this.consumableRepository.Add(consumable);
            this.unitOfWork.Commit();

            return consumable;
        }
    }
}
