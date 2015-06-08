using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Croumetro.Core.Entities;
using Croumetro.Core.Entities.User;
using Croumetro.Database.Tools;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.WinRT;

namespace Croumetro.Database.DataSource
{
    public class UserDataSource : IDisposable
    {
        private const string Dbfilename = "App4321.db";
        protected StorageFolder UserFolder { get; set; }
        protected SQLiteAsyncConnection Db { get; set; }

        public Repository<UserEntity> UserRepository { get; set; }

        public Repository<CroudiaAuthEntity> CroudiaAuthRepository { get; set; }

        public UserDataSource()
        {
            UserFolder = ApplicationData.Current.LocalFolder;
            var dbPath = Path.Combine(UserFolder.Path, Dbfilename);
            var connectionFactory = new Func<SQLiteConnectionWithLock>(() => new SQLiteConnectionWithLock(new SQLitePlatformWinRT(), new SQLiteConnectionString(dbPath, storeDateTimeAsTicks: false)));
            Db = new SQLiteAsyncConnection(connectionFactory);

            UserRepository = new Repository<UserEntity>(Db);

            CroudiaAuthRepository = new Repository<CroudiaAuthEntity>(Db);
        }

        public void InitDatabase()
        {
            //Check to ensure db file exists
            try
            {
                //Try to read the database file
                UserFolder.GetFileAsync(Dbfilename).GetAwaiter().GetResult();
            }
            catch
            {
                //Will throw an exception if not found
                UserFolder.CreateFileAsync(Dbfilename).GetAwaiter().GetResult();
            }
        }

        public void CreateDatabase()
        {
            var existingTables =
                Db.QueryAsync<sqlite_master>("SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;")
                  .GetAwaiter()
                  .GetResult();

            if (existingTables.Any(x => x.name == "UserEntity") != true)
            {
                Db.CreateTableAsync<UserEntity>().GetAwaiter().GetResult();
            }

            if (existingTables.Any(x => x.name == "CroudiaAuthEntity") != true)
            {
                Db.CreateTableAsync<CroudiaAuthEntity>().GetAwaiter().GetResult();
            }
        }

        public void Dispose()
        {
            this.Db = null;
        }
    }
}
