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
    public class GetByPlaceAndNumberOfPeopleShould
    {
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 2)]
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 4)]
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 6)]
        public void GetByPlaceAndNumberOfPeopleShould_CallRepositoryPropertyAll(string placeId, int peopleCount)
        {
            var repositoryMock = new Mock<IRepository<Table>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<ITablesFactory>();

            var service = new Services.TablesService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            var placeIdGuid = new Guid(placeId);

            service.GetByPlaceAndNumberOfPeople(placeIdGuid, peopleCount);
            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 2)]
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 4)]
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 6)]
        public void GetByPlaceAndNumberOfPeopleShould_ReturnNull_WhenThereAreNotSuchTables(string placeId, int peopleCount)
        {
            var repositoryMock = new Mock<IRepository<Table>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<ITablesFactory>();

            var service = new Services.TablesService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            var placeIdGuid = new Guid(placeId);
            var listAll = new List<Table>();

            repositoryMock.Setup(r => r.All).Returns(listAll.AsQueryable());

            var result = service.GetByPlaceAndNumberOfPeople(placeIdGuid, peopleCount);
            Assert.IsNull(result);
        }

        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 2)]
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 4)]
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 6)]
        public void GetByPlaceAndNumberOfPeopleShould_ReturnTables_WhenThereAreSuchTables(string placeId, int peopleCount)
        {
            var repositoryMock = new Mock<IRepository<Table>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<ITablesFactory>();

            var service = new Services.TablesService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            var placeIdGuid = new Guid(placeId);
            var table = new Table() {PlaceId = placeIdGuid, NumberOfPeople = peopleCount};
            var listAll = new List<Table>() {table};

            repositoryMock.Setup(r => r.All).Returns(listAll.AsQueryable());

            var result = service.GetByPlaceAndNumberOfPeople(placeIdGuid, peopleCount);
            Assert.AreSame(table, result);
        }
    }
}
