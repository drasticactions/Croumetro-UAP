using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Croumetro.Common;
using Croumetro.Core.Entities;
using Croumetro.Core.Managers;
using Croumetro.Tools.Manager;
using Croumetro.Tools.Models.Scrolling;

namespace Croumetro.ViewModels
{
    public class HomePageViewModel : NotifierBase
    {
        private StatusManager _statusManager;
        private HomeScrollingCollection _homeScrollingCollection;
        public HomeScrollingCollection HomeScrollingCollection
        {
            get { return _homeScrollingCollection; }
            set
            {
                SetProperty(ref _homeScrollingCollection, value);
                OnPropertyChanged();
            }
        }

        public async Task Initialize()
        {
            HomeScrollingCollection = new HomeScrollingCollection();
        }
    }
}
