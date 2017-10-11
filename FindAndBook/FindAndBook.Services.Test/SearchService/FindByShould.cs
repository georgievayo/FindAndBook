using System.Collections.Generic;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace FindAndBook.Services.Test.SearchService
{
    [TestFixture]
    public class FindByShould
    {
        [TestCase("Restaurant", "Name", "pesho")]
        [TestCase("Club", "Name", "pesho")]
        [TestCase("Cafe", "Name", "pesho")]
        public void FindByShould_CallPlaceServiceMethodFindInName_WhenSearchByIsName(string category, string searchBy, string pattern)
        {
            var placeServiceMock = new Mock<IPlaceService>();

            var service = new Services.SearchService(placeServiceMock.Object);

            service.FindBy(category, searchBy, pattern);

            placeServiceMock.Verify(s => s.FindInName(category, pattern));
        }

        [TestCase("Restaurant", "Name", "pesho")]
        [TestCase("Club", "Name", "pesho")]
        [TestCase("Cafe", "Name", "pesho")]
        public void FindByShould_ReturnCorrectResult_WhenSearchByIsName(string category, string searchBy, string pattern)
        {
            var placeServiceMock = new Mock<IPlaceService>();

            var service = new Services.SearchService(placeServiceMock.Object);
            var place = new Place() {Name = "Pesho's place"};
            var list = new List<Place>() {place};
            placeServiceMock.Setup(s => s.FindInName(category, pattern)).Returns(list.AsQueryable());

            var result = service.FindBy(category, searchBy, pattern);

            Assert.Contains(place, result.ToList());
        }

        [TestCase("Restaurant", "Description", "pesho")]
        [TestCase("Club", "Description", "pesho")]
        [TestCase("Cafe", "Description", "pesho")]
        public void FindByShould_CallPlaceServiceMethodFindInDescription_WhenSearchByIsDescription(string category, string searchBy, string pattern)
        {
            var placeServiceMock = new Mock<IPlaceService>();

            var service = new Services.SearchService(placeServiceMock.Object);

            service.FindBy(category, searchBy, pattern);

            placeServiceMock.Verify(s => s.FindInDescription(category, pattern));
        }

        [TestCase("Restaurant", "Description", "pesho")]
        [TestCase("Club", "Description", "pesho")]
        [TestCase("Cafe", "Description", "pesho")]
        public void FindByShould_ReturnCorrectResult_WhenSearchByIsDescription(string category, string searchBy, string pattern)
        {
            var placeServiceMock = new Mock<IPlaceService>();

            var service = new Services.SearchService(placeServiceMock.Object);
            var place = new Place() { Details = "Pesho's place" };
            var list = new List<Place>() { place };
            placeServiceMock.Setup(s => s.FindInDescription(category, pattern)).Returns(list.AsQueryable());

            var result = service.FindBy(category, searchBy, pattern);

            Assert.Contains(place, result.ToList());
        }

        [TestCase("Restaurant", "Address", "pesho")]
        [TestCase("Club", "Address", "pesho")]
        [TestCase("Cafe", "Address", "pesho")]
        public void FindByShould_CallPlaceServiceMethodFindInAddress_WhenSearchByIsAddress(string category, string searchBy, string pattern)
        {
            var placeServiceMock = new Mock<IPlaceService>();

            var service = new Services.SearchService(placeServiceMock.Object);

            service.FindBy(category, searchBy, pattern);

            placeServiceMock.Verify(s => s.FindInAddress(category, pattern));
        }

        [TestCase("Restaurant", "Address", "pesho")]
        [TestCase("Club", "Address", "pesho")]
        [TestCase("Cafe", "Address", "pesho")]
        public void FindByShould_ReturnCorrectResult_WhenSearchByIsAddress(string category, string searchBy, string pattern)
        {
            var placeServiceMock = new Mock<IPlaceService>();

            var service = new Services.SearchService(placeServiceMock.Object);
            var address = new Address() {Street = "Pesho's street"};
            var place = new Place() { Address = address };
            var list = new List<Place>() { place };
            placeServiceMock.Setup(s => s.FindInAddress(category, pattern)).Returns(list.AsQueryable());

            var result = service.FindBy(category, searchBy, pattern);

            Assert.Contains(place, result.ToList());
        }

        [TestCase("Restaurant", "Average bill", "15")]
        [TestCase("Club", "Average bill", "15")]
        [TestCase("Cafe", "Average bill", "15")]
        public void FindByShould_CallPlaceServiceMethodFindInBill_WhenSearchByIsAverageBill(string category, string searchBy, string pattern)
        {
            var placeServiceMock = new Mock<IPlaceService>();

            var service = new Services.SearchService(placeServiceMock.Object);

            service.FindBy(category, searchBy, pattern);

            placeServiceMock.Verify(s => s.FindInBill(category, pattern));
        }

        [TestCase("Restaurant", "Average bill", "15")]
        [TestCase("Club", "Average bill", "15")]
        [TestCase("Cafe", "Average bill", "15")]
        public void FindByShould_ReturnCorrectResult_WhenSearchByIsAverageBill(string category, string searchBy, string pattern)
        {
            var placeServiceMock = new Mock<IPlaceService>();

            var service = new Services.SearchService(placeServiceMock.Object);
            var place = new Place() { AverageBill = 15 };
            var list = new List<Place>() { place };
            placeServiceMock.Setup(s => s.FindInBill(category, pattern)).Returns(list.AsQueryable());

            var result = service.FindBy(category, searchBy, pattern);

            Assert.Contains(place, result.ToList());
        }
    }
}
