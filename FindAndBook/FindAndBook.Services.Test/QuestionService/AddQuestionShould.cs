using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Services.Test.QuestionService
{
    [TestFixture]
    public class AddQuestionShould
    {
        [TestCase("Test Test", "test@gmail.com", "Booking", "Problem with booking")]
        public void AddQuestionShould_CallFactoryMethodCreateQuestion(string name, string email, string subject, string message)
        {
            var repositoryMock = new Mock<IRepository<Question>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IQuestionFactory>();

            var service = new Services.QuestionService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object);
            service.AddQuestion(name, email, subject, message);

            factoryMock.Verify(f => f.CreateQuestion(name, email, subject, message));
        }

        [TestCase("Test Test", "test@gmail.com", "Booking", "Problem with booking")]
        public void AddQuestionShould_CallRepositoryMethodAdd(string name, string email, string subject, string message)
        {
            var repositoryMock = new Mock<IRepository<Question>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IQuestionFactory>();

            var question = new Question()
            {
                SenderEmail = email,
                SenderName = name,
                Subject = subject,
                QuestionMessage = message
            };

            factoryMock.Setup(f => f.CreateQuestion(name, email, subject, message)).Returns(question);

            var service = new Services.QuestionService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object);
            service.AddQuestion(name, email, subject, message);

            repositoryMock.Verify(f => f.Add(question));
        }

        [TestCase("Test Test", "test@gmail.com", "Booking", "Problem with booking")]
        public void AddQuestionShould_CallUnitOfWorkMethodCommit(string name, string email, string subject, string message)
        {
            var repositoryMock = new Mock<IRepository<Question>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IQuestionFactory>();

            var question = new Question()
            {
                SenderEmail = email,
                SenderName = name,
                Subject = subject,
                QuestionMessage = message
            };

            factoryMock.Setup(f => f.CreateQuestion(name, email, subject, message)).Returns(question);

            var service = new Services.QuestionService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object);
            service.AddQuestion(name, email, subject, message);

            unitOfWorkMock.Verify(f => f.Commit());
        }

        [TestCase("Test Test", "test@gmail.com", "Booking", "Problem with booking")]
        public void AddQuestionShould_ReturnCorrectQuestion(string name, string email, string subject, string message)
        {
            var repositoryMock = new Mock<IRepository<Question>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IQuestionFactory>();

            var question = new Question()
            {
                SenderEmail = email,
                SenderName = name,
                Subject = subject,
                QuestionMessage = message
            };

            factoryMock.Setup(f => f.CreateQuestion(name, email, subject, message)).Returns(question);

            var service = new Services.QuestionService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object);
            var result = service.AddQuestion(name, email, subject, message);

            Assert.AreSame(question, result);
        }
    }
}
