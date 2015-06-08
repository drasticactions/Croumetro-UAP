using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Croumetro.Core.Entities;
using Croumetro.Core.Entities.User;
using Croumetro.Core.Interfaces;
using Croumetro.Core.Tools;
using Newtonsoft.Json;

namespace Croumetro.Core.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private IWebManager _webManager;

        public AuthenticationManager(IWebManager webManager)
        {
            _webManager = webManager;
        }

        public AuthenticationManager()
    : this(new WebManager())
        {
        }

        public async Task<CroudiaAuthEntity> RequestAccessToken(string code)
        {
            var dic = new Dictionary<string, string>
            {
                ["grant_type"] = "authorization_code",
                ["client_id"] = Constants.ConsumerKey,
                ["client_secret"] = Constants.ConsumerSecret,
                ["code"] = code
            };

            var result = await _webManager.PostData(new Uri(EndPoints.OauthToken), new FormUrlEncodedContent(dic));
            if (!result.IsSuccess)
            {
                throw new Exception("Failed to get auth token: " + result.ResultJson);
            }

            var entity = JsonConvert.DeserializeObject<CroudiaAuthEntity>(result.ResultJson);
            entity.ExpireTime = GetUnixTime(DateTime.Now);
            return entity;
        }

        public async Task<CroudiaAuthEntity> RefreshAccessToken(CroudiaAuthEntity authEntity)
        {
            var dic = new Dictionary<string, string>
            {
                ["grant_type"] = "refresh_token",
                ["client_id"] = Constants.ConsumerKey,
                ["client_secret"] = Constants.ConsumerSecret
            };

            var result = await _webManager.PostData(new Uri(EndPoints.OauthToken), new FormUrlEncodedContent(dic));
            if (!result.IsSuccess)
            {
                throw new Exception("Failed to refresh auth token: " + result.ResultJson);
            }
            var entity = JsonConvert.DeserializeObject<CroudiaAuthEntity>(result.ResultJson);
            entity.ExpireTime = GetUnixTime(DateTime.Now);
            return entity;
        }

        public async Task<UserEntity> VerifyAccount(CroudiaAuthEntity authEntity)
        {
            _webManager = new WebManager(authEntity);
            var result = await _webManager.GetData(new Uri(EndPoints.AccountVerify));
            if (!result.IsSuccess)
            {
                throw new Exception("Failed to verify account: " + result.ResultJson);
            }
            return JsonConvert.DeserializeObject<UserEntity>(result.ResultJson);
        }

        private static long GetUnixTime(DateTime time)
        {
            time = time.ToUniversalTime();
            var timeSpam = time - (new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local));
            return (long)timeSpam.TotalSeconds;
        }
    }
}
