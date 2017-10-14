using AutoMapper;
using FindAndBook.Authentication;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Data;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Providers;
using FindAndBook.Providers.Contracts;
using FindAndBook.Services;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using Ninject.Web.Common;

namespace FindAndBook.Web
{
    public class FindAndBookNinjectModule: NinjectModule
    {
        public override void Load()
        {
            // Providers
            this.Bind<IAuthenticationProvider>().To<AuthenticationProvider>().InRequestScope();
            this.Bind<IDateTimeProvider>().To<DateTimeProvider>().InSingletonScope();
            this.Bind<IHttpContextProvider>().To<HttpContextProvider>().InSingletonScope();

            // Factories
            this.Bind<IUserFactory>().ToFactory().InSingletonScope();
            this.Bind<IPlaceFactory>().ToFactory().InSingletonScope();
            this.Bind<IViewModelFactory>().ToFactory().InSingletonScope();
            this.Bind<IAddressFactory>().ToFactory().InSingletonScope();
            this.Bind<IBookingFactory>().ToFactory().InSingletonScope();
            this.Bind<ITablesFactory>().ToFactory().InSingletonScope();
            this.Bind<IBookedTablesFactory>().ToFactory().InSingletonScope();
            this.Bind<IReviewsFactory>().ToFactory().InSingletonScope();
            this.Bind<IConsumableFactory>().ToFactory().InSingletonScope();
            this.Bind<IQuestionFactory>().ToFactory().InSingletonScope();

            // Services
            this.Bind<IPlaceService>().To<PlaceService>().InRequestScope();
            this.Bind<IUserService>().To<UserService>().InRequestScope();
            this.Bind<IAddressService>().To<AddressService>().InRequestScope();
            this.Bind<IBookingService>().To<BookingService>().InRequestScope();
            this.Bind<IConsumableService>().To<ConsumableService>().InRequestScope();
            this.Bind<ITablesService>().To<TablesService>().InRequestScope();
            this.Bind<IBookedTablesService>().To<BookedTablesService>().InRequestScope();
            this.Bind<IReviewsService>().To<ReviewsService>().InRequestScope();
            this.Bind<ISearchService>().To<SearchService>().InRequestScope();
            this.Bind<IQuestionService>().To<QuestionService>().InRequestScope();
            
            // Others
            this.Bind(typeof(IRepository<>)).To(typeof(EFRepository<>)).InRequestScope();
            this.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            //this.Bind<FindAndBookContext>().ToSelf().InRequestScope();
            this.Bind<IFindAndBookContext>().To<FindAndBookContext>().InRequestScope();
            this.Bind<IMapper>().ToMethod(x => Mapper.Instance).InSingletonScope();
        }
    }
}