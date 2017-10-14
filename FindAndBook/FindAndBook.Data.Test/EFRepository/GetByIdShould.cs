using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Data.Contracts;
using FindAndBook.Data.Test.EFRepository.Fake;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Data.Test.EFRepository
{
    [TestFixture]
    public class GetByIdShould
    {
        [TestCase(1)]
        [TestCase(432)]
        public void GetByIdShould_CallDbContextSetFind(int id)
        {
            var mockedSet = new Mock<DbSet<FakeEFRepository>>();

            var mockedDbContext = new Mock<IFindAndBookContext>();
            mockedDbContext.Setup(x => x.DbSet<FakeEFRepository>()).Returns(mockedSet.Object);

            var repository = new EFRepository<FakeEFRepository>(mockedDbContext.Object);

            repository.GetById(id);

            mockedSet.Verify(x => x.Find(id), Times.Once);
        }

        [TestCase(1)]
        [TestCase(432)]
        public void GetByIdShould_ReturnCorrectly(int id)
        {
            var mockedResult = new Mock<FakeEFRepository>();

            var mockedSet = new Mock<DbSet<FakeEFRepository>>();
            mockedSet.Setup(s => s.Find(It.IsAny<object>())).Returns(mockedResult.Object);

            var mockedDbContext = new Mock<IFindAndBookContext>();
            mockedDbContext.Setup(x => x.DbSet<FakeEFRepository>()).Returns(mockedSet.Object);

            var repository = new EFRepository<FakeEFRepository>(mockedDbContext.Object);

            var result = repository.GetById(id);

            Assert.AreSame(mockedResult.Object, result);
        }
    }
}
