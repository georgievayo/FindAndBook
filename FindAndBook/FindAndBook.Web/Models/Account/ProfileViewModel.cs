using System.Collections.Generic;
using AutoMapper;
using FindAndBook.Models;
using FindAndBook.Web.Infrastructure;

namespace FindAndBook.Web.Models.Account
{
    public class ProfileViewModel : IMapFrom<User>, ICustomMap
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, ProfileViewModel>()
                .ForMember(profileViewModel => profileViewModel.Username,
                    cfg => cfg.MapFrom(user => user.UserName))
                .ForMember(profileViewModel => profileViewModel.Email,
                    cfg => cfg.MapFrom(user => user.Email))
                .ForMember(profileViewModel => profileViewModel.FullName,
                    cfg => cfg.MapFrom(user => user.FirstName + " " + user.LastName))
                .ForMember(profileViewModel => profileViewModel.Bookings,
                    cfg => cfg.MapFrom(user => user.Bookings));
        }
    }
}