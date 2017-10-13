using System;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Services.Test.PlaceService
{
    [TestFixture]
    public class CreatePlaceShould
    {
        [TestCase("Rest", "Restaurant", "0877142574", "09:00 - 12:00", "09:00 - 12:00", 
            "some details", 12, "d547a40d - c45f - 4c43 - 99de - 0bfe9199ff95",
            "d547a40d-c45f-4c43-99de-0bfe9199ff95")]
        public void CreatePlaceShould_CallFactoryMethodCreatePlace(string name, string type, string contact,
            string weekendHours, string weekdaayHours, string details, int? averageBill,
            string userId, string addressId)
        {
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var addressIdGuid = new Guid(addressId);
            service.CreatePlace(name, type, contact, weekendHours, weekdaayHours,
                details, averageBill, userId, addressIdGuid);

            factoryMock.Verify(f => f.CreatePlace(name, type, contact, weekendHours, weekdaayHours,
                details, averageBill, userId, addressIdGuid), Times.Once);
        }

        [TestCase("Rest", "Restaurant", "0877142574", "09:00 - 12:00", "09:00 - 12:00",
            "some details", 12, "d547a40d - c45f - 4c43 - 99de - 0bfe9199ff95",
            "d547a40d-c45f-4c43-99de-0bfe9199ff95")]
        public void CreatePlaceShould_CallRepositoryMethodAdd(string name, string type, string contact,
            string weekendHours, string weekdaayHours, string details, int? averageBill,
            string userId, string addressId)
        {
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var addressIdGuid = new Guid(addressId);
            var place = new Place()
            {
                ManagerId = userId,
                AddressId = addressIdGuid, 
                Name = name,
                Type = type,
                Contact = contact,
                WeekendHours = weekendHours,
                WeekdayHours = weekdaayHours,
                Details = details,
                AverageBill = averageBill,
            };
            factoryMock.Setup(f => f.CreatePlace(name, type, contact, weekendHours, weekdaayHours,
                    details, averageBill, userId, addressIdGuid))
                .Returns(place);

            service.CreatePlace(name, type, contact, weekendHours, weekdaayHours,
                details, averageBill, userId, addressIdGuid);

            repositoryMock.Verify(f => f.Add(place), Times.Once);
        }

        [TestCase("Rest", "Restaurant", "0877142574", "09:00 - 12:00", "09:00 - 12:00",
            "some details", 12, "d547a40d - c45f - 4c43 - 99de - 0bfe9199ff95",
            "d547a40d-c45f-4c43-99de-0bfe9199ff95")]
        public void CreatePlaceShould_CallUnitOfWorkMethodACommit(string name, string type, string contact,
            string weekendHours, string weekdaayHours, string details, int? averageBill,
            string userId, string addressId)
        {
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var addressIdGuid = new Guid(addressId);
            var place = new Place()
            {
                ManagerId = userId,
                AddressId = addressIdGuid,
                Name = name,
                Type = type,
                Contact = contact,
                WeekendHours = weekendHours,
                WeekdayHours = weekdaayHours,
                Details = details,
                AverageBill = averageBill,
            };
            factoryMock.Setup(f => f.CreatePlace(name, type, contact, weekendHours, weekdaayHours,
                    details, averageBill, userId, addressIdGuid))
                .Returns(place);

            service.CreatePlace(name, type, contact, weekendHours, weekdaayHours,
                details, averageBill, userId, addressIdGuid);

            unitOfWorkMock.Verify(f => f.Commit(), Times.Once);
        }

        [TestCase("Rest", "Restaurant", "0877142574", "09:00 - 12:00", "09:00 - 12:00",
            "some details", 12, "d547a40d - c45f - 4c43 - 99de - 0bfe9199ff95",
            "d547a40d-c45f-4c43-99de-0bfe9199ff95")]
        public void CreatePlaceShould_ReturnCorrectResult(string name, string type, string contact,
            string weekendHours, string weekdaayHours, string details, int? averageBill,
            string userId, string addressId)
        {
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var addressIdGuid = new Guid(addressId);
            var place = new Place()
            {
                ManagerId = userId,
                AddressId = addressIdGuid,
                Name = name,
                Type = type,
                Contact = contact,
                WeekendHours = weekendHours,
                WeekdayHours = weekdaayHours,
                Details = details,
                AverageBill = averageBill,
            };
            factoryMock.Setup(f => f.CreatePlace(name, type, contact, weekendHours, weekdaayHours,
                    details, averageBill, userId, addressIdGuid))
                .Returns(place);

            var result = service.CreatePlace(name, type, contact, weekendHours, weekdaayHours,
                details, averageBill, userId, addressIdGuid);

            Assert.AreSame(place, result);
        }
    }
}
