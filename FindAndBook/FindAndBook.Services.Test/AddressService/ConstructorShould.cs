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

namespace FindAndBook.Services.Test.AddressService
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ConstructorShould_SetPassedDataCorrectlly_WhenDataIsNotNull()
        {
            var repositoryMock = new Mock<IRepository<Address>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IAddressFactory>();

            var service = new Services.AddressService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            Assert.IsNotNull(service);
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenRepositoryIsNull()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IAddressFactory>();

            Assert.Throws<ArgumentNullException>(() => new Services.AddressService(null,
                unitOfWorkMock.Object, factoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenUnitOfWorkIsNull()
        {
            var repositoryMock = new Mock<IRepository<Address>>();
            var factoryMock = new Mock<IAddressFactory>();

            Assert.Throws<ArgumentNullException>(() => new Services.AddressService(repositoryMock.Object,
                null, factoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenFactoryIsNull()
        {
            var repositoryMock = new Mock<IRepository<Address>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            Assert.Throws<ArgumentNullException>(() => new Services.AddressService(repositoryMock.Object,
                unitOfWorkMock.Object, null));
        }
    }
}
