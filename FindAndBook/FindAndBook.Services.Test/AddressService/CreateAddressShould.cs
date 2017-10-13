using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Services.Test.AddressService
{
    [TestFixture]
    public class CreateAddressShould
    {
        [TestCase("Bulgaria", "Sofia", "Student city", "Boris Stefanov", 5)]
        public void CreateAddressShould_CallFactoryMethodCreateAddress(string country, 
            string city, string area, string street, int number)
        {
            var repositoryMock = new Mock<IRepository<Address>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IAddressFactory>();

            var service = new Services.AddressService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            service.CreateAddress(country, city, area, street, number);

            factoryMock.Verify(f => f.CreateAddress(country, city, area, street, number));
        }

        [TestCase("Bulgaria", "Sofia", "Student city", "Boris Stefanov", 5)]
        public void CreateAddressShould_CallRepositoryMethodAdd(string country,
            string city, string area, string street, int number)
        {
            var repositoryMock = new Mock<IRepository<Address>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IAddressFactory>();

            var service = new Services.AddressService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var address = new Address()
            {
                Country = country,
                City = city,
                Area = area, 
                Street = street,
                Number = number
            };
            factoryMock.Setup(f => f.CreateAddress(country, city, area, street, number)).Returns(address);
            service.CreateAddress(country, city, area, street, number);

            repositoryMock.Verify(r => r.Add(address), Times.Once);
        }

        [TestCase("Bulgaria", "Sofia", "Student city", "Boris Stefanov", 5)]
        public void CreateAddressShould_CallUnitOfWorkMethodCommit(string country,
            string city, string area, string street, int number)
        {
            var repositoryMock = new Mock<IRepository<Address>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IAddressFactory>();

            var service = new Services.AddressService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var address = new Address()
            {
                Country = country,
                City = city,
                Area = area,
                Street = street,
                Number = number
            };
            factoryMock.Setup(f => f.CreateAddress(country, city, area, street, number)).Returns(address);
            service.CreateAddress(country, city, area, street, number);

            unitOfWorkMock.Verify(r => r.Commit(), Times.Once);
        }
    }
}
