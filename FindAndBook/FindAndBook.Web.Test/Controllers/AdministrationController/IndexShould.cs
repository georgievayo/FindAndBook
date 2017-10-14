using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Areas.Administration.Models;
using FindAndBook.Web.Factories;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.AdministrationController
{
    [TestFixture]
    public class IndexShould
    {
        [OneTimeSetUp]
        public static void InitializeMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Place, PlaceViewModel>();
                cfg.CreateMap<Review, ReviewViewModel>();
                cfg.CreateMap<Question, QuestionViewModel>();
            });
        }

        [Test]
        public void IndexShould_CallPlaceServiceMethodGetAll()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var userServiceMock = new Mock<IUserService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var questionServiceMock = new Mock<IQuestionService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Areas.Administration.Controllers.AdministrationController(placeServiceMock.Object,
                reviewsServiceMock.Object, authProviderMock.Object, userServiceMock.Object, 
                questionServiceMock.Object, factoryMock.Object);

            controller.Index();
            placeServiceMock.Verify(s => s.GetAll(), Times.Once);
        }

        [Test]
        public void IndexShould_CallReviewsServiceMethodGetAll()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var userServiceMock = new Mock<IUserService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var questionServiceMock = new Mock<IQuestionService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Areas.Administration.Controllers.AdministrationController(placeServiceMock.Object,
                reviewsServiceMock.Object, authProviderMock.Object, userServiceMock.Object, 
                questionServiceMock.Object, factoryMock.Object);

            controller.Index();
            reviewsServiceMock.Verify(s => s.GetAll(), Times.Once);
        }

        [Test]
        public void IndexShould_CallUserServiceMethodGetAll()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var userServiceMock = new Mock<IUserService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var questionServiceMock = new Mock<IQuestionService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Areas.Administration.Controllers.AdministrationController(placeServiceMock.Object,
                reviewsServiceMock.Object, authProviderMock.Object, userServiceMock.Object, 
                questionServiceMock.Object, factoryMock.Object);

            controller.Index();
            userServiceMock.Verify(s => s.GetAll(), Times.Once);
        }

        [Test]
        public void IndexShould_CallFactoryMethodCreateAllInformationViewModel()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var userServiceMock = new Mock<IUserService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var questionServiceMock = new Mock<IQuestionService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Areas.Administration.Controllers.AdministrationController(placeServiceMock.Object,
                reviewsServiceMock.Object, authProviderMock.Object, userServiceMock.Object, 
                questionServiceMock.Object, factoryMock.Object);

            var places = new List<Place>();
            placeServiceMock.Setup(s => s.GetAll()).Returns(places.AsQueryable());
            var users = new List<User>();
            userServiceMock.Setup(s => s.GetAll()).Returns(users.AsQueryable());
            var reviews = new List<Review>();
            reviewsServiceMock.Setup(s => s.GetAll()).Returns(reviews.AsQueryable());

            var placesModel = new List<PlaceViewModel>();
            var usersModel = new List<UserViewModel>();
            var reviewsModel = new List<ReviewViewModel>();
            var questionsModel = new List<QuestionViewModel>();

            controller.Index();
            factoryMock.Verify(s => s.CreateAllInformationViewModel(usersModel, reviewsModel, placesModel, questionsModel), Times.Once);
        }

        [Test]
        public void IndexShould_ReturnViewWithCorrectModel()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var userServiceMock = new Mock<IUserService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var questionServiceMock = new Mock<IQuestionService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Areas.Administration.Controllers.AdministrationController(placeServiceMock.Object,
                reviewsServiceMock.Object, authProviderMock.Object, userServiceMock.Object, 
                questionServiceMock.Object, factoryMock.Object);

            var places = new List<Place>();
            placeServiceMock.Setup(s => s.GetAll()).Returns(places.AsQueryable());
            var users = new List<User>();
            userServiceMock.Setup(s => s.GetAll()).Returns(users.AsQueryable());
            var reviews = new List<Review>();
            reviewsServiceMock.Setup(s => s.GetAll()).Returns(reviews.AsQueryable());

            var placesModel = new List<PlaceViewModel>();
            var usersModel = new List<UserViewModel>();
            var reviewsModel = new List<ReviewViewModel>();
            var questionsModel = new List<QuestionViewModel>();
            var model = new AllInformationViewModel()
            {
                Places = placesModel,
                Users = usersModel,
                Reviews = reviewsModel
            };
            factoryMock.Setup(s => s.CreateAllInformationViewModel(usersModel, reviewsModel, placesModel, questionsModel)).Returns(model);

            controller
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel(model);
        }
    }
}
