using AutoMapper;
using FindAndBook.Models;
using FindAndBook.Web.Infrastructure;

namespace FindAndBook.Web.Areas.Administration.Models
{
    public class QuestionViewModel: IMapFrom<Question>, ICustomMap
    {
        public string SenderName { get; set; }

        public string SenderEmail { get; set; }

        public string Subject { get; set; }

        public string Question { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Question, QuestionViewModel>()
                .ForMember(placeViewModel => placeViewModel.SenderName,
                    cfg => cfg.MapFrom(place => place.SenderName))
                .ForMember(placeViewModel => placeViewModel.SenderEmail,
                    cfg => cfg.MapFrom(place => place.SenderEmail))
                .ForMember(placeViewModel => placeViewModel.Subject,
                    cfg => cfg.MapFrom(place => place.Subject))
                .ForMember(placeViewModel => placeViewModel.Question,
                    cfg => cfg.MapFrom(place => place.QuestionMessage));
        }
    }
}