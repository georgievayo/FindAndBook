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
    public class GetAllShould
    {
        [Test]
        public void GetAllShould_CallRepositoryPropertyAll()
        {
            var repositoryMock = new Mock<IRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewsFactory>();

            var service = new Services.ReviewsService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            service.GetAll();

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void GetAllShould_ReturnCorrectResult()
        {
            var repositoryMock = new Mock<IRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewsFactory>();

            var review = new Review() {Message = "cool"};
            var all = new List<Review>() {review};
            repositoryMock.Setup(r => r.All).Returns(all.AsQueryable());

            var service = new Services.ReviewsService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            var result = service.GetAll();

            Assert.Contains(review, result.ToList());
        }
    }
}
