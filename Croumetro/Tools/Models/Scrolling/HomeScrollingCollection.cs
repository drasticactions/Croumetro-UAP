using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;
using Croumetro.Core.Entities.Status;
using Croumetro.Core.Managers;
using Croumetro.Tools.Manager;

namespace Croumetro.Tools.Models.Scrolling
{
    public class HomeScrollingCollection : ObservableCollection<StatusEntity>, ISupportIncrementalLoading
    {
        public HomeScrollingCollection()
        {
            HasMoreItems = true;
            IsLoading = false;
        }

        private bool _isLoading;
        private StatusManager _statusManager;
        public bool IsLoading
        {
            get { return _isLoading; }

            private set
            {
                _isLoading = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsLoading"));
            }
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return LoadDataAsync(count).AsAsyncOperation();

        }

        public async Task<LoadMoreItemsResult> LoadDataAsync(uint count)
        {
            if (IsLoading)
            {
                return new LoadMoreItemsResult { Count = count };
            }
            IsLoading = true;
            _statusManager = new StatusManager(new ClientWebManager(Locator.ViewModels.MainPageVm.AuthEntity));

            try
            {
                if (this.Any())
                {
                    var lastEntity = this.Last();
                    var fromBase = await _statusManager.GetHomeTimeline(false, lastEntity.Id, null, null);
                    foreach (var item in fromBase)
                    {
                        Add(item);
                    }
                    HasMoreItems = fromBase.Any();
                }
                else
                {
                    var newestItems = await _statusManager.GetHomeTimeline(false, null, null, null);
                    foreach (var item in newestItems)
                    {
                        Add(item);
                    }
                    HasMoreItems = newestItems.Any();
                }
            }
            catch (Exception)
            {
                HasMoreItems = false;
            }

            IsLoading = false;
            return new LoadMoreItemsResult { Count = count };
        }

        public bool HasMoreItems { get; private set; }
    }
}
