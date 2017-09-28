using AutoMapper;

namespace FindAndBook.Web.Infrastructure
{
    public interface ICustomMap
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
