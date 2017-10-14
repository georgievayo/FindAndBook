using System;
using FindAndBook.Data.Contracts;
using FindAndBook.Data.Test.EFRepository.Fake;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Data.Test.EFRepository
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void TestConstructor_PassDbContextNull_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new EFRepository<FakeEFRepository>(null));
        }

        [Test]
        public void TestConstructor_PassDbContextCorrectly_ShouldNotThrow()
        {
            var mockedDbContext = new Mock<IFindAndBookContext>();

            Assert.DoesNotThrow(() => new EFRepository<FakeEFRepository>(mockedDbContext.Object));
        }

        [Test]
        public void TestConstructor_PassDbContextCorrectly_ShouldInitializeCorrectly()
        {
            var mockedDbContext = new Mock<IFindAndBookContext>();
            
            var repository = new EFRepository<FakeEFRepository>(mockedDbContext.Object);

            Assert.IsNotNull(repository);
        }
    }
}
