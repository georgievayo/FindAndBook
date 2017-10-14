using FindAndBook.Data.Contracts;
using FindAndBook.Data.Test.EFRepository.Fake;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Data.Test.EFRepository
{
    [TestFixture]
    public class DeleteShould
    {
        [Test]
        public void DeleteShould_CallDbContextSetDeleted()
        {
            var mockedDbContext = new Mock<IFindAndBookContext>();

            var repository = new EFRepository<FakeEFRepository>(mockedDbContext.Object);

            var entity = new Mock<FakeEFRepository>();

            repository.Delete(entity.Object);

            mockedDbContext.Verify(c => c.SetDeleted(entity.Object), Times.Once);
        }
    }
}
