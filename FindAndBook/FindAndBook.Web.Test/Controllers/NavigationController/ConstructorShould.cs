using System;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Web.Factories;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Web.Test.Controllers.NavigationController
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ConstructorShould_SetPassedDataCorrectly_WhenDataIsNotNull()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.NavigationController(authProviderMock.Object, factoryMock.Object);

            Assert.IsNotNull(controller);
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenAuthProviderIsNull()
        {
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(
                () => new Web.Controllers.NavigationController(null, factoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenFactoryIsNull()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();

            Assert.Throws<ArgumentNullException>(
                () => new Web.Controllers.NavigationController(authProviderMock.Object, null));
        }
    }
}
