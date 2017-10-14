using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using FindAndBook.Web.Models.Home;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.HomeController
{
    [TestFixture]
    public class SendQuestionShould
    {
        [Test]
        public void SendQuestionShould_ReturnContactView_WhenModelIsNotValid()
        {
            var factoryMock = new Mock<IViewModelFactory>();
            var questionServiceMock = new Mock<IQuestionService>();

            var controller = new Web.Controllers.HomeController(factoryMock.Object, questionServiceMock.Object);
            controller.ModelState.AddModelError("key", "message");

            var model = new ContactViewModel();
            controller.WithCallTo(c => c.SendQuestion(model))
                .ShouldRenderView("Contact")
                .WithModel(model);
        }

        [TestCase("test", "test", "test@gmail.com", "booking", "problem with booking")]
        public void SendQuestionShould_CallQuestionServiceMethodAddQuestion_WhenModelIsValid(string firstName, 
            string lastName, string email, string subject, string message)
        {
            var factoryMock = new Mock<IViewModelFactory>();
            var questionServiceMock = new Mock<IQuestionService>();

            var controller = new Web.Controllers.HomeController(factoryMock.Object, questionServiceMock.Object);

            var model = new ContactViewModel()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email, 
                Subject = subject,
                Question = message
            };

            controller.SendQuestion(model);

            questionServiceMock.Verify(s => s.AddQuestion(firstName + " " + lastName, email, subject, message),
                Times.Once);
        }

        [TestCase("test", "test", "test@gmail.com", "booking", "problem with booking")]
        public void SendQuestionShould_ReturnSentQuestionView_WhenModelIsValid(string firstName,
            string lastName, string email, string subject, string message)
        {
            var factoryMock = new Mock<IViewModelFactory>();
            var questionServiceMock = new Mock<IQuestionService>();

            var controller = new Web.Controllers.HomeController(factoryMock.Object, questionServiceMock.Object);

            var model = new ContactViewModel()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Subject = subject,
                Question = message
            };

            controller
                .WithCallTo(c => c.SendQuestion(model))
                .ShouldRenderView("SentQuestion");
        }
    }
}
