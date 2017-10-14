using FindAndBook.Data.Contracts;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Data.Test.UnitOfWork
{
    [TestFixture]
    public class CommitShould
    {
        [Test]
        public void CommitShould_CallDbContextSaveChanges()
        {
            var mockedDbContext = new Mock<IFindAndBookContext>();

            var unitOfWork = new Data.UnitOfWork(mockedDbContext.Object);

            unitOfWork.Commit();

            mockedDbContext.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}
