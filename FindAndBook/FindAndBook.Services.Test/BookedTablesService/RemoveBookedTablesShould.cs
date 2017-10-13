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
    public class RemoveBookedTablesShould
    {
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95")]
        public void RemoveBookedTablesShould_CallRepositoryPropertyAll(string bookingId)
        {
            var repositoryMock = new Mock<IRepository<BookedTables>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IBookedTablesFactory>();

            var bookingIdGuid = new Guid(bookingId);
            var service = new Services.BookedTablesService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            service.RemoveBookedTables(bookingIdGuid);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95")]
        public void RemoveBookedTablesShould_CallRepositoryMethodDeleteForEachBooking(string bookingId)
        {
            var repositoryMock = new Mock<IRepository<BookedTables>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IBookedTablesFactory>();

            var bookingIdGuid = new Guid(bookingId);
            var bookedTable = new BookedTables() { BookingId = bookingIdGuid };
            var list = new List<BookedTables>() { bookedTable };
            repositoryMock.Setup(r => r.All).Returns(list.AsQueryable());

            var service = new Services.BookedTablesService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            service.RemoveBookedTables(bookingIdGuid);

            repositoryMock.Verify(r => r.Delete(bookedTable), Times.Once);
        }

        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95")]
        public void RemoveBookedTablesShould_CallUnitOfWorkMethodCommitForEachBooking(string bookingId)
        {
            var repositoryMock = new Mock<IRepository<BookedTables>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IBookedTablesFactory>();

            var bookingIdGuid = new Guid(bookingId);
            var bookedTable = new BookedTables() { BookingId = bookingIdGuid };
            var list = new List<BookedTables>() { bookedTable };
            repositoryMock.Setup(r => r.All).Returns(list.AsQueryable());

            var service = new Services.BookedTablesService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            service.RemoveBookedTables(bookingIdGuid);

            unitOfWorkMock.Verify(r => r.Commit(), Times.Once);
        }
    }
}
