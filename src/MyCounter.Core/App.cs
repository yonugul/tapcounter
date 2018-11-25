using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using MyCounter.Core.AppContext;
using MyCounter.Core.ViewModels.Home;

namespace MyCounter.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterType<IAppSettings, AppSettings>();
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<HomeViewModel>();
        }
    }
}
