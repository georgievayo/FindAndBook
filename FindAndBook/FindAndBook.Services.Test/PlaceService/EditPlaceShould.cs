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

namespace FindAndBook.Services.Test.PlaceService
{
    [TestFixture]
    public class EditPlaceShould
    {
        [TestCase("0877142574", "some details", "url", "09:00 - 12:00", "09:00 - 12:00", 12)]
        public void EditPlaceShould_CallRepositoryMethodGetById(string contact, string details,
            string photoUrl, string weekendHours, string weekdaayHours, int? averageBill)
        {
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);
            var id = Guid.NewGuid();
            service.EditPlace(id, contact, details, photoUrl, weekdaayHours, weekendHours, averageBill);

            repositoryMock.Verify(r => r.GetById(id), Times.Once);
        }

        [TestCase("0877142574", "some details", "url", "09:00 - 12:00", "09:00 - 12:00", 12)]
        public void EditPlaceShould_ReturnNull_WhenPlaceWasNotFound(string contact, string details,
            string photoUrl, string weekendHours, string weekdaayHours, int? averageBill)
        {
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);
            var id = Guid.NewGuid();
            repositoryMock.Setup(r => r.GetById(id)).Returns((Place) null);
            
            var result = service.EditPlace(id, contact, details, photoUrl, weekdaayHours, weekendHours, averageBill);

            Assert.IsNull(result);
        }

        [TestCase("0877142574", "some details", "url", "09:00 - 12:00", "09:00 - 12:00", 12)]
        public void EditPlaceShould_CallRepositoryMethodUpdate(string contact, string details,
            string photoUrl, string weekendHours, string weekdaayHours, int? averageBill)
        {
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);
            var id = Guid.NewGuid();
            var place = new Place()
            {
                Id = id,
                Contact = "contact",
                WeekendHours = "00:00 - 00:00",
                WeekdayHours = "00:00 - 00:00",
                Details = "details",
                AverageBill = 0,
            };
            repositoryMock.Setup(r => r.GetById(id)).Returns(place);

            var edittedPlace = place;
            edittedPlace.Contact = contact;
            edittedPlace.WeekendHours = weekendHours;
            edittedPlace.WeekdayHours = weekdaayHours;
            edittedPlace.Details = details;
            edittedPlace.AverageBill = averageBill;
              
            
            service.EditPlace(id, contact, details, photoUrl, weekdaayHours, weekendHours, averageBill);

            repositoryMock.Verify(r => r.Update(edittedPlace), Times.Once);
        }

        [TestCase("0877142574", "some details", "url", "09:00 - 12:00", "09:00 - 12:00", 12)]
        public void EditPlaceShould_CallUnitOfWorkMethodCommit(string contact, string details,
            string photoUrl, string weekendHours, string weekdaayHours, int? averageBill)
        {
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);
            var id = Guid.NewGuid();
            var place = new Place()
            {
                Id = id,
                Contact = "contact",
                WeekendHours = "00:00 - 00:00",
                WeekdayHours = "00:00 - 00:00",
                Details = "details",
                AverageBill = 0,
            };
            repositoryMock.Setup(r => r.GetById(id)).Returns(place);

            service.EditPlace(id, contact, details, photoUrl, weekdaayHours, weekendHours, averageBill);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }

        [TestCase("0877142574", "some details", "url", "09:00 - 12:00", "09:00 - 12:00", 12)]
        public void EditPlaceShould_ReturnCorrectResult(string contact, string details,
            string photoUrl, string weekendHours, string weekdaayHours, int? averageBill)
        {
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);
            var id = Guid.NewGuid();
            var place = new Place()
            {
                Id = id,
                Contact = "contact",
                WeekendHours = "00:00 - 00:00",
                WeekdayHours = "00:00 - 00:00",
                Details = "details",
                AverageBill = 0,
            };
            repositoryMock.Setup(r => r.GetById(id)).Returns(place);

            var edittedPlace = new Place()
            {
                Id = id,
                Contact = contact,
                WeekendHours = weekendHours,
                WeekdayHours = weekdaayHours,
                Details = details,
                AverageBill = averageBill,
            };

            var result = service.EditPlace(id, contact, details, photoUrl, weekdaayHours, weekendHours, averageBill);

            Assert.AreEqual(contact, result.Contact);
            Assert.AreEqual(details, result.Details);
            Assert.AreEqual(photoUrl, result.PhotoUrl);
            Assert.AreEqual(weekdaayHours, result.WeekdayHours);
            Assert.AreEqual(weekendHours, result.WeekendHours);
            Assert.AreEqual(averageBill, result.AverageBill);
        }
    }
}
