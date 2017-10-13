using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Services.Test.BookedTablesService
{
    [TestFixture]
    public class AddBookedTablesShould
    {
        [TestCase(10)]
        [TestCase(2)]
        [TestCase(4)]
        public void AddBookedTablesShould_CallFactoryMethodCreateBooking(int tablesCount)
        {
            var repositoryMock = new Mock<IRepository<BookedTables>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IBookedTablesFactory>();

            var service = new Services.BookedTablesService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var bookingId = Guid.NewGuid();
            var tableId = Guid.NewGuid();

            service.AddBookedTables(bookingId, tableId, tablesCount);

            factoryMock.Verify(f => f.CreateBookedTable(bookingId, tableId, tablesCount));
        }

        [TestCase(10)]
        [TestCase(2)]
        [TestCase(4)]
        public void CreateBookingShould_CallRepositoryMethodAdd(int tablesCount)
        {
            var repositoryMock = new Mock<IRepository<BookedTables>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IBookedTablesFactory>();

            var service = new Services.BookedTablesService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var bookingId = Guid.NewGuid();
            var tableId = Guid.NewGuid();
            var bookedTable = new BookedTables()
            {
                BookingId = bookingId,
                TableId = tableId,
                TablesCount = tablesCount
            };
            factoryMock.Setup(f => f.CreateBookedTable(bookingId, tableId, tablesCount)).Returns(bookedTable);
            service.AddBookedTables(bookingId, tableId, tablesCount);

           repositoryMock.Verify(r => r.Add(bookedTable), Times.Once);
        }
    }
}
