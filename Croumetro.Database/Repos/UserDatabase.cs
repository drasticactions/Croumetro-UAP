using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Croumetro.Core.Entities;
using Croumetro.Core.Entities.User;
using Croumetro.Database.DataSource;

namespace Croumetro.Database.Repos
{
    public class UserDatabase
    {
        public async Task<bool> SaveUserCredentials(CroudiaAuthEntity authEntity)
        {
            using (var db = new UserDataSource())
            {
                var result = await db.CroudiaAuthRepository.Items.Where(node => node.UserId == authEntity.UserId).FirstOrDefaultAsync();
                if (result != null)
                {
                   return await db.CroudiaAuthRepository.Update(authEntity) > 0;
                }

                return await db.CroudiaAuthRepository.Create(authEntity) > 0;
            }
        }

        public async Task<bool> SaveUser(UserEntity user)
        {
            using (var db = new UserDataSource())
            {
                var result = await db.UserRepository.Items.Where(node => node.Id == user.Id).FirstOrDefaultAsync();
                if (result != null)
                {
                    return await db.UserRepository.Update(user) > 0;
                }

                return await db.UserRepository.Create(user) > 0;
            }
        }
    }
}
