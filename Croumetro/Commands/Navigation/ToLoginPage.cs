using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Croumetro.Common;
using Croumetro.Pages;

namespace Croumetro.Commands.Navigation
{
    public class ToLoginPage : AlwaysExecutableCommand
    {
        public override void Execute(object parameter)
        {
            App.RootFrame.Navigate(typeof (LoginPage));
        }
    }
}
