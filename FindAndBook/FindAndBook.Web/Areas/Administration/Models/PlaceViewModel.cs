using System;
using AutoMapper;
using FindAndBook.Models;
using FindAndBook.Web.Infrastructure;

namespace FindAndBook.Web.Areas.Administration.Models
{
    public class PlaceViewModel : IMapFrom<Place>, ICustomMap
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Manager { get; set; }

        public string Address { get; set; }

        public string Type { get; set; }

        public string Contact { get; set; }

        public int AverageBill { get; set; }

        public string Details { get; set; }

        public string WeekendHours { get; set; }

        public string WeekdayHours { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Place, PlaceViewModel>()
                .ForMember(placeViewModel => placeViewModel.Id,
                    cfg => cfg.MapFrom(place => place.Id))
                .ForMember(placeViewModel => placeViewModel.Name,
                    cfg => cfg.MapFrom(place => place.Name))
                .ForMember(placeViewModel => placeViewModel.Manager,
                    cfg => cfg.MapFrom(place => place.Manager.FirstName + " " + place.Manager.LastName))
                .ForMember(placeViewModel => placeViewModel.Address,
                    cfg => cfg.MapFrom(place => place.Address.Country + ", " + place.Address.City + ", "
                    + place.Address.Area + ", " + place.Address.Street + " " + place.Address.Number))
                .ForMember(placeViewModel => placeViewModel.Type,
                    cfg => cfg.MapFrom(place => place.Type))
                .ForMember(placeViewModel => placeViewModel.Contact,
                    cfg => cfg.MapFrom(place => place.Contact))
                .ForMember(placeViewModel => placeViewModel.AverageBill,
                    cfg => cfg.MapFrom(place => place.AverageBill))
                .ForMember(placeViewModel => placeViewModel.Details,
                    cfg => cfg.MapFrom(place => place.Details))
                .ForMember(placeViewModel => placeViewModel.WeekendHours,
                    cfg => cfg.MapFrom(place => place.WeekendHours))
                .ForMember(placeViewModel => placeViewModel.WeekdayHours,
                    cfg => cfg.MapFrom(place => place.WeekdayHours));
        }
    }
}