using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using FindAndBook.Models;
using FindAndBook.Web.Infrastructure;

namespace FindAndBook.Web.Models.Places
{
    public class EditViewModel : IMapFrom<Place>, ICustomMap
    {
        public Guid? Id { get; set; }

        [Display(Name = "Photo url")]
        [AllowHtml]
        public string PhotoUrl { get; set; }

        [Display(Name = "Contact")]
        [AllowHtml]
        public string Contact { get; set; }

        [Display(Name = "Country")]
        [AllowHtml]
        public string Country { get; set; }

        [Display(Name = "City")]
        [AllowHtml]
        public string City { get; set; }

        [Display(Name = "Area")]
        [AllowHtml]
        public string Area { get; set; }

        [Display(Name = "Street")]
        [AllowHtml]
        public string Street { get; set; }

        [Display(Name = "Number")]
        [Range(0, 1000)]
        [AllowHtml]
        public int Number { get; set; }

        [Display(Name = "Description")]
        [MinLength(10)]
        [AllowHtml]
        public string Description { get; set; }

        [Display(Name = "Weekday hours")]
        [AllowHtml]
        public string WeekdayHours { get; set; }

        [Display(Name = "Weekend hours")]
        [AllowHtml]
        public string WeekendHours { get; set; }

        [Display(Name = "Average bill")]
        [AllowHtml]
        public int? AverageBill { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Place, EditViewModel>()
                .ForMember(editViewModel => editViewModel.Id,
                    cfg => cfg.MapFrom(place => place.Id))
                .ForMember(editViewModel => editViewModel.Country,
                    cfg => cfg.MapFrom(place => place.Address.Country))
                .ForMember(editViewModel => editViewModel.City,
                    cfg => cfg.MapFrom(place => place.Address.City))
                .ForMember(editViewModel => editViewModel.Area,
                    cfg => cfg.MapFrom(place => place.Address.Area))
                .ForMember(editViewModel => editViewModel.Street,
                    cfg => cfg.MapFrom(place => place.Address.Street))
                .ForMember(editViewModel => editViewModel.Number,
                    cfg => cfg.MapFrom(place => place.Address.Number))
                .ForMember(editViewModel => editViewModel.PhotoUrl,
                    cfg => cfg.MapFrom(place => place.PhotoUrl))
                .ForMember(editViewModel => editViewModel.Contact,
                    cfg => cfg.MapFrom(place => place.Contact))
                .ForMember(editViewModel => editViewModel.Description,
                    cfg => cfg.MapFrom(place => place.Details))
                .ForMember(editViewModel => editViewModel.AverageBill,
                    cfg => cfg.MapFrom(place => place.AverageBill));
        }
    }
}