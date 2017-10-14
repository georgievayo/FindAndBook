using System;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Services.Test.AddressService
{
    [TestFixture]
    public class EditAddressShould
    {
        [TestCase("Bulgaria", "Sofia", "Student city", "Boris Stefanov", 5)]
        public void EditAddressShould_CallRepositoryMethodGetById(string country, string city, string area, string street, int number)
        {
            var repositoryMock = new Mock<IRepository<Address>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IAddressFactory>();

            var id = Guid.NewGuid();

            var service = new Services.AddressService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            service.EditAddress(id, country, city, area, street, number);

            repositoryMock.Verify(r => r.GetById(id), Times.Once);
        }

        [TestCase("Bulgaria", "Sofia", "Student city", "Boris Stefanov", 5)]
        public void EditAddressShould_ReturnNull_WhenAddressWasNotFound(string country, string city, string area, string street, int number)
        {
            var repositoryMock = new Mock<IRepository<Address>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IAddressFactory>();

            var id = Guid.NewGuid();
            repositoryMock.Setup(r => r.GetById(id)).Returns((Address) null);

            var service = new Services.AddressService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var result = service.EditAddress(id, country, city, area, street, number);

            Assert.IsNull(result);
        }

        [TestCase("Bulgaria", "Sofia", "Student city", "Boris Stefanov", 5)]
        public void EditAddressShould_CallRepositoryMethodUpdate_WhenAddressWasFound(string country, string city, string area, string street, int number)
        {
            var repositoryMock = new Mock<IRepository<Address>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IAddressFactory>();

            var id = Guid.NewGuid();
            var address = new Address()
            {
                Id = id,
                Country = country,
                City = city,
                Street = street,
                Area = area,
                Number = number
            };

            repositoryMock.Setup(r => r.GetById(id)).Returns(address);

            var service = new Services.AddressService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var result = service.EditAddress(id, country, city, area, street, number);

            repositoryMock.Verify(r => r.Update(address), Times.Once);
        }

        [TestCase("Bulgaria", "Sofia", "Student city", "Boris Stefanov", 5)]
        public void EditAddressShould_CallUnitOfWorkMethodCommit_WhenAddressWasFound(string country, string city, string area, string street, int number)
        {
            var repositoryMock = new Mock<IRepository<Address>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IAddressFactory>();

            var id = Guid.NewGuid();
            var address = new Address()
            {
                Id = id,
                Country = country,
                City = city,
                Street = street,
                Area = area,
                Number = number
            };

            repositoryMock.Setup(r => r.GetById(id)).Returns(address);

            var service = new Services.AddressService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            service.EditAddress(id, country, city, area, street, number);

            unitOfWorkMock.Verify(r => r.Commit(), Times.Once);
        }

        [TestCase("Bulgaria", "Sofia", "Student city", "Boris Stefanov", 5)]
        public void EditAddressShould_ReturnAddress_WhenAddressWasFound(string country, string city, string area, string street, int number)
        {
            var repositoryMock = new Mock<IRepository<Address>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IAddressFactory>();

            var id = Guid.NewGuid();
            var address = new Address()
            {
                Id = id,
                Country = country,
                City = city,
                Street = street,
                Area = area,
                Number = number
            };

            repositoryMock.Setup(r => r.GetById(id)).Returns(address);

            var service = new Services.AddressService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var result = service.EditAddress(id, country, city, area, street, number);

            Assert.AreSame(address, result);
        }
    }
}
