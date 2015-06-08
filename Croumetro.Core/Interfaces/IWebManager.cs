using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Croumetro.Core.Entities;
using Croumetro.Core.Managers;

namespace Croumetro.Core.Interfaces
{
    public interface IWebManager
    {
        bool IsNetworkAvailable { get; }

        CroudiaAuthEntity GetAuthEntity();

        Task<WebManager.Result> PostData(Uri uri, HttpContent content);

        Task<WebManager.Result> PutData(Uri uri, StringContent json);

        Task<WebManager.Result> DeleteData(Uri uri, StringContent json);

        Task<WebManager.Result> GetData(Uri uri);
    }
}
