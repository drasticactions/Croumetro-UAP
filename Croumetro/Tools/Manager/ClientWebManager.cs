using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Croumetro.Core.Entities;
using Croumetro.Core.Interfaces;
using Croumetro.Core.Managers;
using Croumetro.Database.Repos;

namespace Croumetro.Tools.Manager
{
    public class ClientWebManager : WebManager
    {
        private readonly AuthenticationManager _authManager = new AuthenticationManager();
        private readonly UserDatabase _db = new UserDatabase();
        public ClientWebManager(CroudiaAuthEntity authEntity = null)
        {
            AuthEntity = authEntity;
        }
        public override async Task<Result> GetData(Uri uri)
        {
            if (string.IsNullOrEmpty(AuthEntity?.AccessToken)) return await base.GetData(uri);
            if (GetUnixTime(DateTime.Now) - AuthEntity.ExpireTime <= AuthEntity.ExpiresIn)
                return await base.GetData(uri);

            var result = await _authManager.RefreshAccessToken(AuthEntity);

            result.User = AuthEntity.User;
            result.UserId = AuthEntity.UserId;

            await _db.SaveUserCredentials(result);

            AuthEntity = result;

            return await base.GetData(uri);
        }

        public override async Task<Result> PostData(Uri uri, HttpContent content)
        {
            if (string.IsNullOrEmpty(AuthEntity?.AccessToken)) return await base.GetData(uri);
            if (GetUnixTime(DateTime.Now) - AuthEntity.ExpireTime <= AuthEntity.ExpiresIn)
                return await base.PostData(uri, content);

            var result = await _authManager.RefreshAccessToken(AuthEntity);

            result.User = AuthEntity.User;
            result.UserId = AuthEntity.UserId;

            await _db.SaveUserCredentials(result);

            AuthEntity = result;

            return await base.PostData(uri, content);
        }

        private static long GetUnixTime(DateTime time)
        {
            time = time.ToUniversalTime();
            var timeSpam = time - (new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local));
            return (long)timeSpam.TotalSeconds;
        }
    }
}
