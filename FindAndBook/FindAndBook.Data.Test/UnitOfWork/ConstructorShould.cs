using System;
using FindAndBook.Data.Contracts;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Data.Test.UnitOfWork
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenPassedDbContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Data.UnitOfWork(null));
        }

        [Test]
        public void ConstructorShould_NotThrowException_WhenPassedDbContextIsNotNull()
        {
            var mockedDbContext = new Mock<IFindAndBookContext>();

            Assert.DoesNotThrow(() => new Data.UnitOfWork(mockedDbContext.Object));
        }

        [Test]
        public void ConstructorShould_InitializeCorrectly_WhenPassedDbContextIsNotNull()
        {
            var mockedDbContext = new Mock<IFindAndBookContext>();
            
            var unitOfWork = new Data.UnitOfWork(mockedDbContext.Object);

            Assert.IsNotNull(unitOfWork);
        }
    }
}
