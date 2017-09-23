using FindAndBook.Authentication;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Factories;
using Ninject.Extensions.Factory;
using Ninject.Modules;

namespace FindAndBook.Web
{
    public class FindAndBookNinjectModule: NinjectModule
    {
        public override void Load()
        {
            this.Bind<IAuthenticationProvider>().To<AuthenticationProvider>().InSingletonScope();
            this.Bind<IUserFactory>().ToFactory().InSingletonScope();
        }
    }
}