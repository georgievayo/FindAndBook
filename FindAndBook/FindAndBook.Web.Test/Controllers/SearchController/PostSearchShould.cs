using FindAndBook.Services.Contracts;
using FindAndBook.Web.Models.Search;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.SearchController
{
    [TestFixture]
    public class PostSearchShould
    {
        [TestCase("Restaurant", "Name", "pesho")]
        public void PostSearchShould_RedirectToGetSearchWithCorrectParameters(string category,
            string searchBy, string pattern)
        {
            var serviceMock = new Mock<ISearchService>();
            var controller = new Web.Controllers.SearchController(serviceMock.Object);
            var viewModel = new SearchViewModel()
            {
                Category = category,
                SearchBy = searchBy,
                Pattern = pattern
            };

            controller.WithCallTo(c => c.Search(viewModel))
                .ShouldRedirectTo((Web.Controllers.SearchController c) => c.Search(viewModel.Category,
                    viewModel.SearchBy, viewModel.Pattern, 10, 1));
        }
    }
}
