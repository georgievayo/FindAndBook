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

namespace FindAndBook.Web
{
    public class FindAndBookNinjectModule: NinjectModule
    {
        public override void Load()
        {
            // Providers
            this.Bind<IAuthenticationProvider>().To<AuthenticationProvider>().InSingletonScope();
            this.Bind<IDateTimeProvider>().To<DateTimeProvider>().InSingletonScope();
            this.Bind<IHttpContextProvider>().To<HttpContextProvider>().InSingletonScope();

            // Factories
            this.Bind<IUserFactory>().ToFactory().InSingletonScope();
            this.Bind<IPlaceFactory>().ToFactory().InSingletonScope();
            this.Bind<IViewModelFactory>().ToFactory().InSingletonScope();
            this.Bind<IAddressFactory>().ToFactory().InSingletonScope();

            // Services
            this.Bind<IPlaceService>().To<PlaceService>().InSingletonScope();
            this.Bind<IUserService>().To<UserService>().InSingletonScope();
            this.Bind<IAddressService>().To<AddressService>().InSingletonScope();

            this.Bind(typeof(IRepository<>)).To(typeof(EFRepository<>)).InSingletonScope();
            this.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
        }
    }
}