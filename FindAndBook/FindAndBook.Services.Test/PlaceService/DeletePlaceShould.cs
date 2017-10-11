using System;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Services.Test.PlaceService
{
    [TestFixture]
    public class DeletePlaceShould
    {
        [Test]
        public void DeletePlaceShould_CallRepositoryMethodGetById()
        {
            var id = Guid.NewGuid();
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            service.DeletePlace(id);

            repositoryMock.Verify(r => r.GetById(id), Times.Once);
        }

        [Test]
        public void DeletePlaceShould_CallRepositoryMethodDelete()
        {
            var id = Guid.NewGuid();
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();

            var place = new Place() {Id = id};
            repositoryMock.Setup(r => r.GetById(id)).Returns(place);

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            service.DeletePlace(id);

            repositoryMock.Verify(r => r.Delete(place), Times.Once);
        }

        [Test]
        public void DeletePlaceShould_CallUnitOfWorkMethodCommit()
        {
            var id = Guid.NewGuid();
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();

            var place = new Place() { Id = id };
            repositoryMock.Setup(r => r.GetById(id)).Returns(place);

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            service.DeletePlace(id);

            unitOfWorkMock.Verify(r => r.Commit(), Times.Once);
        }
    }
}
