using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Croumetro.Core.Entities;
using Croumetro.Core.Interfaces;
using Croumetro.Core.Tools;

namespace Croumetro.Core.Managers
{
    public class WebManager : IWebManager
    {
        public WebManager(CroudiaAuthEntity authEntity = null)
        {
            AuthEntity = authEntity;
        }
        public class Result
        {
            public Result(bool isSuccess, string json)
            {
                IsSuccess = isSuccess;
                ResultJson = json;
            }

            public bool IsSuccess { get; private set; }
            public string ResultJson { get; private set; }
        }

        public bool IsNetworkAvailable => NetworkInterface.GetIsNetworkAvailable();

        public static CroudiaAuthEntity AuthEntity { get; set; }
        public CroudiaAuthEntity GetAuthEntity()
        {
            return AuthEntity;
        }

        public async virtual Task<Result> PostData(Uri uri, HttpContent content)
        {
            var httpClient = new HttpClient();
            try
            {
                if (!string.IsNullOrEmpty(AuthEntity?.AccessToken))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthEntity?.AccessToken);
                }
                var response = await httpClient.PostAsync(uri, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                return new Result(response.IsSuccessStatusCode, responseContent);
            }
            catch (Exception ex)
            {
                throw new WebException("Croudia API Error: Service error", ex);
            }
        }

        public async Task<Result> PutData(Uri uri, StringContent json)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> DeleteData(Uri uri, StringContent json)
        {
            throw new NotImplementedException();
        }

        public async virtual Task<Result> GetData(Uri uri)
        {
            var httpClient = new HttpClient();

            try
            {
                if (!string.IsNullOrEmpty(AuthEntity?.AccessToken))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthEntity?.AccessToken);
                }
                var response = await httpClient.GetAsync(uri);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new WebException("Croudia API Error: Service not found.");
                }
                var responseContent = await response.Content.ReadAsStringAsync();
                return string.IsNullOrEmpty(responseContent) ? new Result(response.IsSuccessStatusCode, string.Empty) : new Result(response.IsSuccessStatusCode, responseContent);
            }
            catch (Exception ex)
            {

                throw new WebException("Croudia API Error: Service error", ex);
            }
        }
    }
}
