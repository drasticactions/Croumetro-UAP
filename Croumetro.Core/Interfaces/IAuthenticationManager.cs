using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Croumetro.Core.Entities;
using Croumetro.Core.Entities.User;

namespace Croumetro.Core.Interfaces
{
    public interface IAuthenticationManager
    {
        Task<CroudiaAuthEntity> RequestAccessToken(string code);

        Task<CroudiaAuthEntity> RefreshAccessToken(CroudiaAuthEntity authEntity);

        Task<UserEntity> VerifyAccount(CroudiaAuthEntity authEntity);
    }
}
