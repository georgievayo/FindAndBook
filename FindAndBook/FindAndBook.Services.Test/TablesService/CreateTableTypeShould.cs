using System;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Services.Test.TablesService
{
    [TestFixture]
    public class CreateTableTypeShould
    {
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 2, 5)]
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 4, 5)]
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 6, 5)]
        public void CreateTableTypeShould_CallFactoryMethodCreateTableType(string placeId, int numberOfPeople, int numberOfTables)
        {
            var repositoryMock = new Mock<IRepository<Table>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<ITablesFactory>();

            var service = new Services.TablesService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            var placeIdGuid = new Guid(placeId);

            service.CreateTableType(placeIdGuid, numberOfPeople, numberOfTables);

            factoryMock.Verify(f => f.CreateTableType(placeIdGuid, numberOfPeople, numberOfTables), Times.Once);
        }

        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 2, 5)]
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 4, 5)]
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 6, 5)]
        public void CreateTableTypeShould_CallRepositoryMethodAdd(string placeId, int numberOfPeople, int numberOfTables)
        {
            var repositoryMock = new Mock<IRepository<Table>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<ITablesFactory>();
            
            var service = new Services.TablesService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            var placeIdGuid = new Guid(placeId);
            var table = new Table()
            {
                PlaceId = placeIdGuid,
                NumberOfPeople = numberOfPeople,
                NumberOfTables = numberOfTables
            };
            factoryMock.Setup(x => x.CreateTableType(placeIdGuid, numberOfPeople, numberOfTables)).Returns(table);
            service.CreateTableType(placeIdGuid, numberOfPeople, numberOfTables);

            repositoryMock.Verify(f => f.Add(table), Times.Once);
        }

        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 2, 5)]
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 4, 5)]
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 6, 5)]
        public void CreateTableTypeShould_CallUnitOfWorkMethodCommit(string placeId, int numberOfPeople, int numberOfTables)
        {
            var repositoryMock = new Mock<IRepository<Table>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<ITablesFactory>();

            var service = new Services.TablesService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            var placeIdGuid = new Guid(placeId);
            var table = new Table()
            {
                PlaceId = placeIdGuid,
                NumberOfPeople = numberOfPeople,
                NumberOfTables = numberOfTables
            };

            factoryMock.Setup(x => x.CreateTableType(placeIdGuid, numberOfPeople, numberOfTables)).Returns(table);
            service.CreateTableType(placeIdGuid, numberOfPeople, numberOfTables);

            unitOfWorkMock.Verify(f => f.Commit(), Times.Once);
        }

        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 2, 5)]
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 4, 5)]
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 6, 5)]
        public void CreateTableTypeShould_ReturnCorrectTableType(string placeId, int numberOfPeople, int numberOfTables)
        {
            var repositoryMock = new Mock<IRepository<Table>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<ITablesFactory>();

            var service = new Services.TablesService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            var placeIdGuid = new Guid(placeId);
            var table = new Table()
            {
                PlaceId = placeIdGuid,
                NumberOfPeople = numberOfPeople,
                NumberOfTables = numberOfTables
            };
            factoryMock.Setup(x => x.CreateTableType(placeIdGuid, numberOfPeople, numberOfTables)).Returns(table);
            var result = service.CreateTableType(placeIdGuid, numberOfPeople, numberOfTables);

            Assert.AreSame(table, result);
        }
    }
}
