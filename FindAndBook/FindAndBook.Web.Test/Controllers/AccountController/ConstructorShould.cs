using System;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Services.Contracts;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Web.Test.Controllers.AccountController
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ConstructorShould_ThrowException_WhenAuthProviderIsNull()
        {
            var userServiceMock = new Mock<IUserService>();

            Assert.Throws<ArgumentNullException>(() => new Web.Controllers.AccountController(null, userServiceMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenServiceIsNull()
        {

            var authProviderMock = new Mock<IAuthenticationProvider>();

            Assert.Throws<ArgumentNullException>(() => new Web.Controllers.AccountController(authProviderMock.Object, null));
        }


        [Test]
        public void ConstructorShould_SetPassedData_WhenDataIsNotNull()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var mockedFactory = new Mock<IUserService>();

            var controller = new Web.Controllers.AccountController(authProviderMock.Object, mockedFactory.Object);

            Assert.IsNotNull(controller);
        }
    }
}
