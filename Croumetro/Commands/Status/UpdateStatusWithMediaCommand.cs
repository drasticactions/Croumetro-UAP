using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Croumetro.Common;
using Croumetro.Core.Managers;
using Croumetro.Tools.Manager;

namespace Croumetro.Commands.Status
{
    public class UpdateStatusWithMediaCommand : AlwaysExecutableCommand
    {
        public async override void Execute(object parameter)
        {
            var vm = Locator.ViewModels.MainPageVm;
            if (string.IsNullOrEmpty(vm.StatusMessage) || vm.ImageFile == null)
            {
                return;
            }

            var statusManager = new StatusManager(new ClientWebManager(Locator.ViewModels.MainPageVm.AuthEntity));
            var stream = await vm.ImageFile.OpenAsync(FileAccessMode.Read);
            var result = await statusManager.UpdateStatusWithMedia(vm.StatusMessage, "", stream, null, false);
            if (result != null)
            {
                Locator.ViewModels.HomePageVm.HomeScrollingCollection.Insert(0, result);
            }

        }
    }
}
