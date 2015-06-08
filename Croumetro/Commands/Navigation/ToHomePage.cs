using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Croumetro.Common;
using Croumetro.Pages.List;

namespace Croumetro.Commands.Navigation
{
    public class ToHomePage : AlwaysExecutableCommand
    {
        public async override void Execute(object parameter)
        {
            App.RootFrame.Navigate(typeof (HomePage));
            await Locator.ViewModels.HomePageVm.Initialize();
        }
    }
}
