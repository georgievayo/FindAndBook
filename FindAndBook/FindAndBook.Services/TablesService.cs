using System;
using System.Linq;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;

namespace FindAndBook.Services
{
    public class TablesService : ITablesService
    {
        private readonly IRepository<Table> tablesRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ITablesFactory factory;

        public TablesService(IRepository<Table> tablesRepository, IUnitOfWork unitOfWork, ITablesFactory factory)
        {
            if (tablesRepository == null)
            {
                throw new ArgumentNullException(nameof(tablesRepository));
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            this.tablesRepository = tablesRepository;
            this.unitOfWork = unitOfWork;
            this.factory = factory;
        }

        public Table CreateTableType(Guid? placeId, int numberOfPeople, int numberOfTables)
        {
            var tableType = this.factory.CreateTableType(placeId, numberOfPeople, numberOfTables);
            this.tablesRepository.Add(tableType);
            this.unitOfWork.Commit();
            return tableType;
        }

        public int GetTablesCount(Guid? id, int peopleCount)
        {
            var firstOrDefault = this.tablesRepository
                .All
                .FirstOrDefault(x => x.PlaceId == id && x.NumberOfPeople == peopleCount);
            if (firstOrDefault == null)
            {
                return 0;
            }

            return firstOrDefault.NumberOfTables;
        }

        public Table GetByPlaceAndNumberOfPeople(Guid? placeId, int numberOfPeople)
        {
            return this.tablesRepository
                .All
                .FirstOrDefault(x => x.PlaceId == placeId && x.NumberOfPeople == numberOfPeople);
        }
    }
}
