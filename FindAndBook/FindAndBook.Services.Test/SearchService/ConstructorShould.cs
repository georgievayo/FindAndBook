using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Services.Contracts;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Services.Test.SearchService
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ConstructorShould_SetPassedDataCorrectlly_WhenDataIsNotNull()
        {
            var placeServiceMock = new Mock<IPlaceService>();

            var service = new Services.SearchService(placeServiceMock.Object);

            Assert.IsNotNull(service);
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenDataIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Services.SearchService(null));
        }
    }
}
