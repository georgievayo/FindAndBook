using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Web.Test.Controllers.HomeController
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ConstructorShould_SetPassedDataCorrectly_WhenDataIsNotNull()
        {
            var factoryMock = new Mock<IViewModelFactory>();
            var questionServiceMock = new Mock<IQuestionService>();

            var controller = new Web.Controllers.HomeController(factoryMock.Object, questionServiceMock.Object);

            Assert.IsNotNull(controller);
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenFactoryIsNull()
        {
            var questionServiceMock = new Mock<IQuestionService>();

            Assert.Throws<ArgumentNullException>(() => new Web.Controllers.HomeController(null, questionServiceMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenQuestionServiceIsNull()
        {
            var factoryeMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new Web.Controllers.HomeController(factoryeMock.Object, null));
        }
    }
}
