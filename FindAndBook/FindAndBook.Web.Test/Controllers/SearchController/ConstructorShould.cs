using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Services.Contracts;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Web.Test.Controllers.SearchController
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ConstructorShould_SetPassedDataCorrectlly_WhenDataIsNotNull()
        {
            var serviceMock = new Mock<ISearchService>();

            var controller = new Web.Controllers.SearchController(serviceMock.Object);

            Assert.IsNotNull(controller);
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenServiceIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Web.Controllers.SearchController(null));
        }
    }
}
