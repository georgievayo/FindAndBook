using System;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Services.Test.QuestionService
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ConstructorShould_SetPassedData_WhenDataIsNotNull()
        {
            var repositoryMock = new Mock<IRepository<Question>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IQuestionFactory>();

            var service = new Services.QuestionService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object);

            Assert.IsNotNull(service);
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenRepositoryIsNull()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IQuestionFactory>();

            Assert.Throws<ArgumentNullException>(() => new Services.QuestionService(null, unitOfWorkMock.Object,
                factoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenUnitOfWorkIsNull()
        {
            var repositoryMock = new Mock<IRepository<Question>>();
            var factoryMock = new Mock<IQuestionFactory>();

            Assert.Throws<ArgumentNullException>(() => new Services.QuestionService(repositoryMock.Object, null,
                factoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenFactoryIsNull()
        {
            var repositoryMock = new Mock<IRepository<Question>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            Assert.Throws<ArgumentNullException>(() => new Services.QuestionService(repositoryMock.Object, unitOfWorkMock.Object,
                null));
        }
    }
}
