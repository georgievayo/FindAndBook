using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FindAndBook.Models;
using FindAndBook.Web.Infrastructure;
using FindAndBook.Web.Models.Reviews;

namespace FindAndBook.Web.Models.Places
{
    public class DetailsViewModel : IMapFrom<Place>, ICustomMap
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Address Address { get; set; }

        public string PhotoUrl { get; set; }

        public string Contact { get; set; }

        public string WeekendHours { get; set; }

        public string WeekdayHours { get; set; }

        public string Details { get; set; }

        public int? AverageBill { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public double Rating { get; set; }

        public SingleReviewViewModel ReviewForm { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Place, DetailsViewModel>()
                .ForMember(detailsViewModel => detailsViewModel.Id,
                    cfg => cfg.MapFrom(place => place.Id))
                .ForMember(detailsViewModel => detailsViewModel.Name,
                    cfg => cfg.MapFrom(place => place.Name))
                .ForMember(detailsViewModel => detailsViewModel.PhotoUrl,
                    cfg => cfg.MapFrom(place => place.PhotoUrl))
                .ForMember(detailsViewModel => detailsViewModel.Contact,
                    cfg => cfg.MapFrom(place => place.Contact))
                .ForMember(detailsViewModel => detailsViewModel.Details,
                    cfg => cfg.MapFrom(place => place.Details))
                .ForMember(detailsViewModel => detailsViewModel.AverageBill,
                    cfg => cfg.MapFrom(place => place.AverageBill))
                .ForMember(detailsViewModel => detailsViewModel.Reviews,
                    cfg => cfg.MapFrom(place => place.Reviews))
                .ForMember(detailsViewModel => detailsViewModel.Rating,
                cfg => cfg.MapFrom(place => place.Reviews.Count == 0? 0: place.Reviews.Average(x => x.Rating)));
        }
    }
}