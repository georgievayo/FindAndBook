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

namespace FindAndBook.Services.Test.ReviewsService
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ConstructorShould_SetPassedDataCorrectlly_WhenDataIsNotNull()
        {
            var repositoryMock = new Mock<IRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewsFactory>();

            var service = new Services.ReviewsService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);

            Assert.IsNotNull(service);
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenPassedRepositoryIsNull()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewsFactory>();

            Assert.Throws<ArgumentNullException>(
                () => new Services.ReviewsService(null, unitOfWorkMock.Object, factoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenPassedUnitOfWorkIsNull()
        {
            var repositoryMock = new Mock<IRepository<Review>>();
            var factoryMock = new Mock<IReviewsFactory>();

            Assert.Throws<ArgumentNullException>(
                () => new Services.ReviewsService(repositoryMock.Object, null, factoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenPassedFactoryIsNull()
        {
            var repositoryMock = new Mock<IRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            Assert.Throws<ArgumentNullException>(
                () => new Services.ReviewsService(repositoryMock.Object, unitOfWorkMock.Object, null));
        }
    }
}
