using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Security.Authentication.Web;
using Croumetro.Commands.Navigation;
using Croumetro.Common;
using Croumetro.Core.Interfaces;
using Croumetro.Core.Managers;
using Croumetro.Core.Tools;
using Croumetro.Database.Repos;
using Croumetro.Tools.Manager;

namespace Croumetro.ViewModels
{
    public class LoginPageViewModel : NotifierBase
    {
        private IAuthenticationManager _authManager;

        public LoginPageViewModel()
        {
            _authManager = new AuthenticationManager();
            ClickLoginButtonCommand = new AsyncDelegateCommand(async o => { await ClickLoginButton(); },
                o => CanClickLoginButton);
        }

        public bool CanClickLoginButton => !IsLoading;

        public ToMainPage ToMainPage { get; set; } = new ToMainPage();

        public AsyncDelegateCommand ClickLoginButtonCommand { get; private set; }

        public event EventHandler<EventArgs> LoginSuccessful;
        public event EventHandler<EventArgs> LoginFailed;

        private string _loginUrl =
            "https://api.croudia.com/oauth/authorize?response_type=code&client_id=88af26dcf4225aa781c7d58af5c6f6aa5e3a4d64f8ddfb31d86bc5a663efed61&state=77f094743bdb55c0f97";

        private string _callback = "https://sites.google.com/site/slowbeeflogin/";
        private readonly UserDatabase _userDatabase = new UserDatabase();

        public async Task ClickLoginButton()
        {
            IsLoading = true;
            var result =
                await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, new Uri(_loginUrl),
                    new Uri(_callback));
            var loginResult = result.ResponseStatus == WebAuthenticationStatus.Success;

            if (loginResult)
            {

                var decoder = Extensions.ParseQueryString(result.ResponseData);
                var code = decoder.FirstOrDefault();
                if (string.IsNullOrEmpty(code.Value))
                {
                    loginResult = false;
                }
                else
                {
                    try
                    {
                        var authEntity = await _authManager.RequestAccessToken(code.Value);
                        if (authEntity == null)
                        {
                            loginResult = false;
                        }
                        else
                        {
                            _authManager = new AuthenticationManager(new ClientWebManager(authEntity));
                            var user = await _authManager.VerifyAccount(authEntity);
                            if (user == null)
                            {
                                loginResult = false;
                            }
                            else
                            {
                                authEntity.User = user;
                                authEntity.UserId = user.Id;
                                authEntity.IsDefault = true;
                                await _userDatabase.SaveUser(user);
                                await _userDatabase.SaveUserCredentials(authEntity);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // TODO Show error message
                        throw;
                    }
                    
                }
            }

            IsLoading = false;
            base.RaiseEvent(loginResult ? LoginSuccessful : LoginFailed, EventArgs.Empty);
        }
    }
}
