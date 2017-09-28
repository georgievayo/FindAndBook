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

            // Services
            this.Bind<IPlaceService>().To<PlaceService>().InRequestScope();
            this.Bind<IUserService>().To<UserService>().InRequestScope();
            this.Bind<IAddressService>().To<AddressService>().InRequestScope();

            this.Bind(typeof(IRepository<>)).To(typeof(EFRepository<>)).InRequestScope();
            this.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            this.Bind<FindAndBookContext>().ToSelf().InRequestScope();
            this.Bind<IMapper>().ToMethod(x => Mapper.Instance).InSingletonScope();
        }
    }
}