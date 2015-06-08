using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Autofac;
using Croumetro.Common;
using Croumetro.ViewModels;

namespace Croumetro.Locator
{
    public class ViewModels
    {
        public ViewModels()
        {
            if (DesignMode.DesignModeEnabled)
            {
                App.Container = AutoFacConfiguration.Configure();
            }
        }
        public static LoginPageViewModel LoginPageVm => App.Container.Resolve<LoginPageViewModel>();

        public static MainPageViewModel MainPageVm => App.Container.Resolve<MainPageViewModel>();
    }
}
