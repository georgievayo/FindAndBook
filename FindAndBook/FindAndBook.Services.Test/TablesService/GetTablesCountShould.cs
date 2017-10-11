using System;
using System.Collections.Generic;
using System.Linq;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Services.Test.TablesService
{
    [TestFixture]
    public class GetTablesCountShould
    {
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 2)]
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 4)]
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 6)]
        public void GetTablesCountShould_CallRepositoryPropertyAll(string placeId, int peopleCount) 
        {
            var repositoryMock = new Mock<IRepository<Table>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<ITablesFactory>();

            var service = new Services.TablesService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            var placeIdGuid = new Guid(placeId);

            service.GetTablesCount(placeIdGuid, peopleCount);
            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 2)]
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 4)]
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 6)]
        public void GetTablesCountShould_ReturnZero_WhenThereIsNoTablesInThisPlace(string placeId, int peopleCount)
        {
            var repositoryMock = new Mock<IRepository<Table>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<ITablesFactory>();
            var listAll = new List<Table>();

            var service = new Services.TablesService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            var placeIdGuid = new Guid(placeId);
            repositoryMock
                .Setup(r => r.All)
                .Returns(listAll.AsQueryable<Table>());

            var result = service.GetTablesCount(placeIdGuid, peopleCount);

            Assert.AreEqual(0, result);
        }

        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 2)]
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 4)]
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 6)]
        public void GetTablesCountShould_ReturnZero_WhenThereIsTableInThisPlace(string placeId, int peopleCount)
        {
            var repositoryMock = new Mock<IRepository<Table>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<ITablesFactory>();

            var service = new Services.TablesService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            var placeIdGuid = new Guid(placeId);
            var table = new Table()
            {
                PlaceId = placeIdGuid,
                NumberOfPeople = peopleCount,
                NumberOfTables = 12
            };
            var listAll = new List<Table>() {table};

            repositoryMock
                .Setup(r => r.All)
                .Returns(listAll.AsQueryable<Table>());

            var result = service.GetTablesCount(placeIdGuid, peopleCount);

            Assert.AreEqual(12, result);
        }
    }
}
