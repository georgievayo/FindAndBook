using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Services.Test.QuestionService
{
    [TestFixture]
    public class GetAllShould
    {
        [Test]
        public void GetAllShould_CallRepositoryPropertyAll()
        {
            var repositoryMock = new Mock<IRepository<Question>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IQuestionFactory>();

            var service = new Services.QuestionService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object);

            service.GetAll();

            repositoryMock.Verify(r => r.All, Times.Once);
        }
    }
}
