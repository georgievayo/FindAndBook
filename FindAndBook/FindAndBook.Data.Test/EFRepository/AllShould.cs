using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FindAndBook.Data.Contracts;
using FindAndBook.Data.Test.EFRepository.Fake;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Data.Test.EFRepository
{
    [TestFixture]
    public class AllShould
    {
        private IQueryable<FakeEFRepository> GetData()
        {
            return new List<FakeEFRepository>
            {
                new FakeEFRepository(),
                new FakeEFRepository(),
                new FakeEFRepository()
            }.AsQueryable();
        }

        [Test]
        public void AllShould_CallDbContextSet()
        {
            var data = this.GetData();

            var mockedSet = new Mock<IDbSet<FakeEFRepository>>();
            mockedSet.Setup(m => m.Provider).Returns(data.Provider);
            mockedSet.Setup(m => m.Expression).Returns(data.Expression);
            mockedSet.Setup(m => m.ElementType).Returns(data.ElementType);
            mockedSet.Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockedDbContext = new Mock<IFindAndBookContext>();
            mockedDbContext.Setup(x => x.DbSet<FakeEFRepository>()).Returns(mockedSet.Object);

            var repository = new EFRepository<FakeEFRepository>(mockedDbContext.Object);

            var result = repository.All;

            mockedDbContext.Verify(db => db.DbSet<FakeEFRepository>(), Times.Once);
        }
    }
}
