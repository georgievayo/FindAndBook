﻿using System;
using System.Data.Entity;
using System.Linq;
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

        public BookedTables GetBookedTable(Guid? bookingId)
        {
            return this.bookedTablesRepository
                .All
                .Include(x => x.Table)
                .FirstOrDefault(x => x.BookingId == bookingId);
        }

        public void RemoveBookedTables(Guid? bookingId)
        {
            var bookedTables = this.bookedTablesRepository.All.Where(x => x.BookingId == bookingId).ToList();
            foreach (var bookedTable in bookedTables)
            {
                this.bookedTablesRepository.Delete(bookedTable);
                this.unitOfWork.Commit();
            }
        }
    }
}
