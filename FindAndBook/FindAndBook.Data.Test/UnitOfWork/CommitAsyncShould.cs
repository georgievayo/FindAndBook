using FindAndBook.Data.Contracts;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Data.Test.UnitOfWork
{
    [TestFixture]
    public class CommitAsyncShould
    {
        [Test]
        public void CommitAsyncShould_CallDbContextSaveChangesAsync()
        {
            var mockedDbContext = new Mock<IFindAndBookContext>();

            var unitOfWork = new Data.UnitOfWork(mockedDbContext.Object);

            unitOfWork.CommitAsync();

            mockedDbContext.Verify(c => c.SaveChangesAsync(), Times.Once);
        }
    }
}
