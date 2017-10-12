using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Models.Places;
using Moq;
using NUnit.Framework;
using PagedList;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.SearchController
{
    [TestFixture]
    public class GetSearchShould
    {
        private void InitializeMapper()
        {
            Mapper.Initialize(cfg =>
                cfg.CreateMap<Place, PlaceShortViewModel>()
                    .ForMember(viewModel => viewModel.Name, 
                    opt => opt.MapFrom(place => place.Name))
                    //.ForMember(viewModel => viewModel.Contact, opt => opt.Ignore())
                    //.ForMember(viewModel => viewModel.PhotoUrl, opt => opt.Ignore())
                    //.ForMember(viewModel => viewModel.Id, opt => opt.MapFrom(place => place.Id))
                    //.ForMember(viewModel => viewModel.Rating, opt => opt.Ignore())
                    //.ForMember(viewModel => viewModel.ReviewsCount, opt => opt.Ignore())
                    //.ForMember(viewModel => viewModel.AverageBill, opt => opt.Ignore())
                    //.ForMember(viewModel => viewModel.Address, opt => opt.Ignore())
            );
        }

        [TestCase("Restaurant", "Name", "pesho")]
        public void GetSearchShould_CallSearchServiceMethodFindAll(string category,
            string searchBy, string pattern)
        {
            var serviceMock = new Mock<ISearchService>();
            InitializeMapper();

            var controller = new Web.Controllers.SearchController(serviceMock.Object);

            controller.Search(category, searchBy, pattern);

            serviceMock.Verify(s => s.FindBy(category, searchBy, pattern), Times.Once);
        }

        [TestCase("Restaurant", "Name", "pesho")]
        public void GetSearchShould_ReturnCorrectView(string category,
            string searchBy, string pattern)
        {
            var serviceMock = new Mock<ISearchService>();
            InitializeMapper();

            var list = new List<Place>()
            {
                new Place()
                {
                    Name = "Pesho's restaurant",
                    Type = "Restaurant"
                }
            };
            var expectedModel = new List<PlaceShortViewModel>()
            {
                new PlaceShortViewModel()
                {
                    Name = "Pesho's restaurant"
                }
            }.ToPagedList(1, 1);
       

        serviceMock.Setup(s => s.FindBy(category, searchBy, pattern))
                .Returns(list.AsQueryable());

            var controller = new Web.Controllers.SearchController(serviceMock.Object);

            controller
                .WithCallTo(c => c.Search(category, searchBy, pattern, 1, 1))
                .ShouldRenderView("List");
                //.WithModel<PagedList.IPagedList<FindAndBook.Web.Models.Places.PlaceShortViewModel>>(m =>
                //{
                //    CollectionAssert.AreEqual(expectedModel, m);
                //});
        }
    }
}
