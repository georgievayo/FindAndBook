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
    public class FindShould
    {
        [TestCase("Restaurant", "pesho")]
        [TestCase("Club", "pesho")]
        [TestCase("Cafe", "pesho")]
        public void FindInNameShould_CallRepositoryPropertyAll(string category, string pattern)
        {
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            service.FindInName(category, pattern);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [TestCase("Restaurant", "pesho")]
        [TestCase("Club", "pesho")]
        [TestCase("Cafe", "pesho")]
        public void FindInNameShould_ReturnCorrectValue(string category, string pattern)
        {
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();
            var list = new List<Place>()
            {
                new Place()
                {
                    Type = "Restaurant",
                    Name = "Pesho's restaurant"
                },
                new Place()
                {
                    Type = "Club",
                    Name = "Pesho's club"
                },
                new Place()
                {
                    Type = "Cafe",
                    Name = "Pesho's cafe"
                }
            };

            repositoryMock.Setup(r => r.All).Returns(list.AsQueryable());

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var result = service.FindInName(category, pattern);

            Assert.AreEqual(1, result.ToList().Count);
        }

        [TestCase("Restaurant", "pesho")]
        [TestCase("Club", "pesho")]
        [TestCase("Cafe", "pesho")]
        public void FindInDescriptionShould_CallRepositoryPropertyAll(string category, string pattern)
        {
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            service.FindInDescription(category, pattern);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [TestCase("Restaurant", "pesho")]
        [TestCase("Club", "pesho")]
        [TestCase("Cafe", "pesho")]
        public void FindInDescriptionShould_ReturnCorrectValue(string category, string pattern)
        {
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();
            var list = new List<Place>()
            {
                new Place()
                {
                    Type = "Restaurant",
                    Details = "Pesho's restaurant"
                },
                new Place()
                {
                    Type = "Club",
                    Details = "Pesho's club"
                },
                new Place()
                {
                    Type = "Cafe",
                    Details = "Pesho's cafe"
                }
            };

            repositoryMock.Setup(r => r.All).Returns(list.AsQueryable());

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var result = service.FindInDescription(category, pattern);

            Assert.AreEqual(1, result.ToList().Count);
        }

        [TestCase("Restaurant", "pesho")]
        [TestCase("Club", "pesho")]
        [TestCase("Cafe", "pesho")]
        public void FindInAddressShould_CallRepositoryPropertyAll(string category, string pattern)
        {
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            service.FindInAddress(category, pattern);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [TestCase("Restaurant", "pesho")]
        [TestCase("Club", "pesho")]
        [TestCase("Cafe", "pesho")]
        public void FindInAddressShould_ReturnCorrectValue(string category, string pattern)
        {
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();
            var list = new List<Place>()
            {
                new Place()
                {
                    Type = "Restaurant",
                    Address = new Address()
                    {
                        Country = "Bulgaria",
                        City = "Sofia",
                        Street = "PeshoStreet",
                        Area = "mladost"
                    }
                },
                new Place()
                {
                    Type = "Club",
                    Address = new Address()
                    {
                        Country = "Pesholand",
                        City = "Sofia",
                        Street = "Aleksandar Malinov",
                        Area = "mladost"
                    }
                },
                new Place()
                {
                    Type = "Cafe",
                    Address = new Address()
                    {
                        City = "Pesho's city",
                        Country = "Bulgaria",
                        Street = "PeshoStreet",
                        Area = "mladost"
                    }
                }
            };

            repositoryMock.Setup(r => r.All).Returns(list.AsQueryable());

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var result = service.FindInAddress(category, pattern);

            Assert.AreEqual(1, result.ToList().Count);
        }

        [TestCase("Restaurant", "1")]
        [TestCase("Club", "1")]
        [TestCase("Cafe", "1")]
        public void FindInBillShould_CallRepositoryPropertyAll(string category, string pattern)
        {
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            service.FindInBill(category, pattern);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [TestCase("Restaurant", "1")]
        [TestCase("Club", "1")]
        [TestCase("Cafe", "1")]
        public void FindInBillShould_ReturnCorrectValue(string category, string pattern)
        {
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();
            var list = new List<Place>()
            {
                new Place()
                {
                    Type = "Restaurant",
                    AverageBill = 1
                },
                new Place()
                {
                    Type = "Club",
                    AverageBill = 1
                },
                new Place()
                {
                    Type = "Cafe",
                    AverageBill = 1
                }
            };

            repositoryMock.Setup(r => r.All).Returns(list.AsQueryable());

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var result = service.FindInBill(category, pattern);

            Assert.AreEqual(1, result.ToList().Count);
        }
    }
}
