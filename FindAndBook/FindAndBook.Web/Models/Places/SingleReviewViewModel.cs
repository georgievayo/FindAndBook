using System;
using AutoMapper;
using FindAndBook.Models;
using FindAndBook.Web.Infrastructure;

namespace FindAndBook.Web.Models.Places
{
    public class SingleReviewViewModel : IMapFrom<Review>, ICustomMap
    {
        public string Message { get; set; }

        public string Username { get; set; }

        public DateTime PostedOn { get; set; }
        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Review, SingleReviewViewModel>()
                .ForMember(reviewViewModel => reviewViewModel.Message,
                    cfg => cfg.MapFrom(review => review.Message))
                .ForMember(reviewViewModel => reviewViewModel.Username,
                    cfg => cfg.MapFrom(review => review.User.UserName))
                .ForMember(reviewViewModel => reviewViewModel.PostedOn,
                    cfg => cfg.MapFrom(review => review.PostedOn));
        }
    }
}