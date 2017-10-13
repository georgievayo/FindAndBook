using System;
using System.Collections.Generic;
using System.Linq;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Services.Test.BookedTablesService
{
    [TestFixture]
    public class GetBookedTableShould
    {
        [Test]
        public void GetBookedTableShould_CallRepositoryMethodAll()
        {
            var repositoryMock = new Mock<IRepository<BookedTables>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IBookedTablesFactory>();

            var service = new Services.BookedTablesService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);
            var id = Guid.NewGuid();

            service.GetBookedTable(id);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void GetBookedTableShould_ReturnCorrectResult()
        {
            var repositoryMock = new Mock<IRepository<BookedTables>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IBookedTablesFactory>();

            var id = Guid.NewGuid();
            var bookedTable = new BookedTables() { BookingId = id };
            var list = new List<BookedTables>() { bookedTable };
            repositoryMock.Setup(r => r.All).Returns(list.AsQueryable());

            var service = new Services.BookedTablesService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var result = service.GetBookedTable(id);

            Assert.AreSame(bookedTable, result);
        }
    }
}
