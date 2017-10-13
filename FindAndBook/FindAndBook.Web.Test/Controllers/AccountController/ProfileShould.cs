using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Models.Account;
using FindAndBook.Web.Models.Places;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.AccountController
{
    [TestFixture]
    public class ProfileShould
    {
        [Test]
        public void ProfileShould_ReturnErrorView_WhenPassedUsernameIsNull()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var userServiceMock = new Mock<IUserService>();
            InitializeMapper();
            var controller = new Web.Controllers.AccountController(authProviderMock.Object, userServiceMock.Object);

            controller
                .WithCallTo(c => c.Profile(null))
                .ShouldRenderView("Error");
        }

        [TestCase("pesho")]
        [TestCase("gosho")]
        [TestCase("tosho")]
        public void ProfileShould_CallUserServiceMethodGetUserByUsername_WhenPassedUsernameIsNotNull(string username)
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var userServiceMock = new Mock<IUserService>();
            InitializeMapper();

            var controller = new Web.Controllers.AccountController(authProviderMock.Object, userServiceMock.Object);

            controller.Profile(username);

            userServiceMock.Verify(s => s.GetUserByUsername(username), Times.Once);
        }

        [TestCase("pesho")]
        [TestCase("gosho")]
        [TestCase("tosho")]
        public void ProfileShould_ReturnErrorView_WhenUserWasNotFound(string username)
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var userServiceMock = new Mock<IUserService>();
            var emptyList = new List<User>();
            userServiceMock.Setup(s => s.GetUserByUsername(username)).Returns(emptyList.AsQueryable());
            InitializeMapper();

            var controller = new Web.Controllers.AccountController(authProviderMock.Object, userServiceMock.Object);

            controller
                .WithCallTo(c => c.Profile(username))
                .ShouldRenderView("Error");

        }

        [TestCase("pesho")]
        [TestCase("gosho")]
        [TestCase("tosho")]
        public void ProfileShould_ReturnDefaultView_WhenUserWasFound(string username)
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var userServiceMock = new Mock<IUserService>();
            var user = new User()
            {
                UserName = username
            };
            var list = new List<User>() {user};
            userServiceMock.Setup(s => s.GetUserByUsername(username)).Returns(list.AsQueryable());
            InitializeMapper();
            authProviderMock.Setup(ap => ap.CurrentUserUsername).Returns(username);
            var controller = new Web.Controllers.AccountController(authProviderMock.Object, userServiceMock.Object);
            var model = new ProfileViewModel()
            {
                IsCurrentUser = true
            };
            controller
                .WithCallTo(c => c.Profile(username))
                .ShouldRenderDefaultView();
        }

        private void InitializeMapper()
        {
            Mapper.Initialize(cfg =>
                cfg.CreateMap<User, ProfileViewModel>()
                    .ForMember(viewModel => viewModel.Username,
                        opt => opt.MapFrom(user => user.UserName))
            );
        }
    }
}
