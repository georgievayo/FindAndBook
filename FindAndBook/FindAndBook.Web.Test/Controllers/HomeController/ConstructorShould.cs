using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            var controller = new Web.Controllers.HomeController(factoryMock.Object);

            Assert.IsNotNull(controller);
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenDataIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Web.Controllers.HomeController(null));
        }
    }
}
