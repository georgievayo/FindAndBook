using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;

namespace FindAndBook.Services
{
    public class BookedTablesService : IBookedTablesService
    {
        private readonly IRepository<BookedTables> bookedTablesRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IBookedTablesFactory factory;

        public BookedTablesService(IRepository<BookedTables> bookedTablesRepository,
            IUnitOfWork unitOfWork, IBookedTablesFactory factory)
        {
            if (bookedTablesRepository == null)
            {
                throw new ArgumentNullException(nameof(bookedTablesRepository));
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            this.bookedTablesRepository = bookedTablesRepository;
            this.unitOfWork = unitOfWork;
            this.factory = factory;
        }
        public void AddBookedTables(Guid? bookingId, Guid? tableId, int tablesCount)
        {
            var bookedTable = this.factory.CreateBookedTable(bookingId, tableId, tablesCount);
            this.bookedTablesRepository.Add(bookedTable);
            this.unitOfWork.Commit();
        }
    }
}
