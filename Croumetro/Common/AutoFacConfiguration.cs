using Autofac;
using Croumetro.ViewModels;

namespace Croumetro.Common
{
    public class AutoFacConfiguration
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            // Register View Models
            builder.RegisterType<LoginPageViewModel>().SingleInstance();
            builder.RegisterType<MainPageViewModel>().SingleInstance();
            builder.RegisterType<HomePageViewModel>().SingleInstance();
            //builder.RegisterType<MainPage>();
            return builder.Build();
        }
    }
}
