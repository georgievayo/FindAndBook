using System;
using AutoMapper;
using FindAndBook.Models;
using FindAndBook.Web.Infrastructure;

namespace FindAndBook.Web.Areas.Administration.Models
{
    public class ReviewViewModel : IMapFrom<Review>, ICustomMap
    {
        public Guid? Id { get; set; }

        public string PlaceName { get; set; }

        public string UserNames { get; set; }

        public string Message { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Review, ReviewViewModel>()
                .ForMember(reviewViewModel => reviewViewModel.Id,
                    cfg => cfg.MapFrom(review => review.Id))
                .ForMember(reviewViewModel => reviewViewModel.PlaceName,
                    cfg => cfg.MapFrom(review => review.Place.Name))
                .ForMember(reviewViewModel => reviewViewModel.UserNames,
                    cfg => cfg.MapFrom(review => review.User.FirstName + " " + review.User.LastName))
                .ForMember(reviewViewModel => reviewViewModel.Message,
                    cfg => cfg.MapFrom(review => review.Message));
        }
    }
}