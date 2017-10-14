using FindAndBook.Data.Test.EFRepository.Fake;
using Moq;
using NUnit.Framework;
using FindAndBook.Data.Contracts;

namespace FindAndBook.Data.Test.EFRepository
{
    [TestFixture]
    public class AddShould
    {
        [Test]
        public void AddShould_CallDbContextSetAdded()
        {
            var dbContextMock = new Mock<IFindAndBookContext>();

            var repository = new EFRepository<FakeEFRepository>(dbContextMock.Object);

            var entity = new Mock<FakeEFRepository>();

            repository.Add(entity.Object);

            dbContextMock.Verify(c => c.SetAdded(entity.Object), Times.Once);
        }
    }
}
