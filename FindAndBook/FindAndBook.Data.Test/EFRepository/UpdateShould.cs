using FindAndBook.Data.Contracts;
using FindAndBook.Data.Test.EFRepository.Fake;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Data.Test.EFRepository
{
    [TestFixture]
    public class UpdateShould
    {
        [Test]
        public void UpdateShould_CallDbContextSetUpdated()
        {
            var mockedDbContext = new Mock<IFindAndBookContext>();

            var repository = new EFRepository<FakeEFRepository>(mockedDbContext.Object);

            var entity = new Mock<FakeEFRepository>();

            repository.Update(entity.Object);

            mockedDbContext.Verify(c => c.SetUpdated(entity.Object), Times.Once);
        }
    }
}
