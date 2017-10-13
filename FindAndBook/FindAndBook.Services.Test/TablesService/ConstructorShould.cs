using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using FindAndBook.Services;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Services.Test.TablesService
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ConstructorShould_SetPassedDataCorrectlly_WhenDataIsNotNull()
        {
            var repositoryMock = new Mock<IRepository<Table>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<ITablesFactory>();

            var service = new Services.TablesService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);

            Assert.IsNotNull(service);
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenPassedRepositoryIsNull()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<ITablesFactory>();

            Assert.Throws<ArgumentNullException>(
                () => new Services.TablesService(null, unitOfWorkMock.Object, factoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenPassedUnitOfWorkIsNull()
        {
            var repositoryMock = new Mock<IRepository<Table>>();
            var factoryMock = new Mock<ITablesFactory>();

            Assert.Throws<ArgumentNullException>(
                () => new Services.TablesService(repositoryMock.Object, null, factoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenPassedFactoryIsNull()
        {
            var repositoryMock = new Mock<IRepository<Table>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            Assert.Throws<ArgumentNullException>(
                () => new Services.TablesService(repositoryMock.Object, unitOfWorkMock.Object, null));
        }
    }
}
