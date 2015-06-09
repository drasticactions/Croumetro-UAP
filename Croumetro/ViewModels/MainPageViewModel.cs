using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Croumetro.Commands.Navigation;
using Croumetro.Commands.Status;
using Croumetro.Common;
using Croumetro.Core.Entities;
using Croumetro.Core.Entities.User;
using Croumetro.Core.Managers;
using Croumetro.Database.Repos;
using Croumetro.Tools.Manager;
using Croumetro.Tools.Models;

namespace Croumetro.ViewModels
{
    public class MainPageViewModel : NotifierBase
    {
        private UserDatabase _db = new UserDatabase();
        private AuthenticationManager _authManager;
        private UserEntity _user;
        private CroudiaAuthEntity _authEntity;
        private string _statusMessage;
        private StorageFile _imageFile;
        public ToLoginPage ToLoginPage { get; set; } = new ToLoginPage();

        public UpdateStatusWithMediaCommand UpdateStatusWithMediaCommand { get; set; } =
            new UpdateStatusWithMediaCommand();
        public StorageFile ImageFile
        {
            get { return _imageFile; }
            set
            {
                SetProperty(ref _imageFile, value);
                OnPropertyChanged();
            }
        }
        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                SetProperty(ref _statusMessage, value);
                OnPropertyChanged();
            }
        }
        public UserEntity User
        {
            get { return _user; }
            set
            {
                SetProperty(ref _user, value);
                OnPropertyChanged();
            }
        }

        public CroudiaAuthEntity AuthEntity
        {
            get { return _authEntity; }
            set
            {
                SetProperty(ref _authEntity, value);
                OnPropertyChanged();
            }
        }
        public List<MenuItem> MenuItems { get; set; }

        public async Task<bool> LoginTest()
        {
            IsLoading = true;
            var result = await _db.GetDefaultUserEntity();
            if (result != null)
            {
                var clientWeb = new ClientWebManager(result);
                _authManager = new AuthenticationManager(clientWeb);
                var updateUser = await _authManager.VerifyAccount(result);
                await _db.SaveUser(updateUser);
                AuthEntity = clientWeb.GetAuthEntity();
                User = updateUser;
            }

            return result != null;
        }

        public MainPageViewModel()
        {
            MenuItems = new List<MenuItem>()
            {
                new MenuItem()
                {
                    Icon = "\uE7F8",
                    Name = "ホーム",
                    Command = new ToHomePage()
                },
                new MenuItem()
                {
                    Icon = "\uE8F1",
                    Name = "返信",
                    //Command = new NavigateToBookmarksCommand()
                }
                ,
                new MenuItem()
                {
                    Icon = "\uE15C",
                    Name = "Public",
                   // Command = new NavigateToTabPageCommand()
                },
                new MenuItem()
                {
                    Icon = "\uE91C",
                    Name = "メール",
                    //Command = new NavigateToPrivateMessageListPageCommand()
                },
                new MenuItem()
                {
                    Icon = "\uE8A1",
                    Name = "アルバム",
                    //Command = new NavigateToSaclopedia()
                },
                new MenuItem()
                {
                    Icon = "\uE721",
                    Name = "検索",
                    //Command = new NavigateToSearchPageCommand()
                },
                new MenuItem()
                {
                    Icon = "\uE713",
                    Name = "設定",
                    //Command = new NavigateToSettingsCommand()
                }
                ,
                new MenuItem()
                {
                    Icon = "\uE77B",
                    Name = "アプリについて",
                    //Command = new NavigateToAboutPage()
                }
            };
        }
    }
}
