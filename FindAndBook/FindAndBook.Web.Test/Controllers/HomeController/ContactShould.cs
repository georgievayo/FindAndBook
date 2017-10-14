using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.HomeController
{
    [TestFixture]
    public class ContactShould
    {
        [Test]
        public void ContactShould_ReturnDefaultView()
        {
            var factoryMock = new Mock<IViewModelFactory>();
            var questionServiceMock = new Mock<IQuestionService>();

            var controller = new Web.Controllers.HomeController(factoryMock.Object, questionServiceMock.Object);

            controller.WithCallTo(c => c.Contact())
                .ShouldRenderDefaultView();
        }
    }
}
