using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using FindAndBook.Web.Models.Search;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.HomeController
{
    [TestFixture]
    public class IndexShould
    {
        [Test]
        public void IndexShould_CallFactoryMethodCreateSearchViewModel()
        {
            var factoryMock = new Mock<IViewModelFactory>();
            var questionServiceMock = new Mock<IQuestionService>();

            var controller = new Web.Controllers.HomeController(factoryMock.Object, questionServiceMock.Object);

            controller.Index();

            factoryMock.Verify(f => f.CreateSearchViewModel(), Times.Once);
        }

        [Test]
        public void IndexShould_ReturnViewWithCorrectModel()
        {
            var factoryMock = new Mock<IViewModelFactory>();
            var questionServiceMock = new Mock<IQuestionService>();

            var model = new SearchViewModel();
            factoryMock.Setup(f => f.CreateSearchViewModel()).Returns(model);

            var controller = new Web.Controllers.HomeController(factoryMock.Object, questionServiceMock.Object);
            controller
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel(model);
        }
    }
}
