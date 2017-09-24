using FindAndBook.Authentication;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Factories;
using FindAndBook.Providers;
using FindAndBook.Providers.Contracts;
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

            // Services
        }
    }
}