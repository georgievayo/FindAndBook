using FindAndBook.Authentication;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Factories;
using FindAndBook.Providers;
using FindAndBook.Providers.Contracts;
using Ninject.Extensions.Factory;
using Ninject.Modules;

namespace FindAndBook.Web
{
    public class FindAndBookNinjectModule: NinjectModule
    {
        public override void Load()
        {
            this.Bind<IAuthenticationProvider>().To<AuthenticationProvider>().InSingletonScope();
            this.Bind<IDateTimeProvider>().To<DateTimeProvider>().InSingletonScope();
            this.Bind<IHttpContextProvider>().To<HttpContextProvider>().InSingletonScope();
            this.Bind<IUserFactory>().ToFactory().InSingletonScope();
        }
    }
}