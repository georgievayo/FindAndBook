using System;
using System.Linq;
using AutoMapper;
using FindAndBook.Models;
using FindAndBook.Web.Infrastructure;

namespace FindAndBook.Web.Models.Places
{
    public class PlaceShortViewModel : IMapFrom<Place>, ICustomMap
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string PhotoUrl { get; set; }

        public Address Address { get; set; }

        public string Contact { get; set; }

        public int? AverageBill { get; set; }

        public int ReviewsCount { get; set; }

        public double Rating { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Place, PlaceShortViewModel>()
                .ForMember(shortViewModel => shortViewModel.Id,
                    cfg => cfg.MapFrom(place => place.Id))
                .ForMember(shortViewModel => shortViewModel.Name,
                    cfg => cfg.MapFrom(place => place.Name))
                .ForMember(shortViewModel => shortViewModel.PhotoUrl,
                    cfg => cfg.MapFrom(place => place.PhotoUrl))
                .ForMember(shortViewModel => shortViewModel.Contact,
                    cfg => cfg.MapFrom(place => place.Contact))
                .ForMember(shortViewModel => shortViewModel.AverageBill,
                    cfg => cfg.MapFrom(place => place.AverageBill))
                .ForMember(shortViewModel => shortViewModel.ReviewsCount,
                    cfg => cfg.MapFrom(place => place.Reviews.Count))
                .ForMember(shortViewModel => shortViewModel.Rating,
                    cfg => cfg.MapFrom(place => place.Reviews.Count == 0 ? 0: place.Reviews.Average(x => x.Rating)));
        }
    }
}