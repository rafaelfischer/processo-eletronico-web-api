using Newtonsoft.Json;
using Prodest.ProcessoEletronico.Integration.Common;
using Prodest.ProcessoEletronico.Integration.Common.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Infraestrutura.Integrations
{
    public class ApiHandler : IApiHandler
    {
        IClientAccessTokenProvider _accessTokenProvider;
        public ApiHandler(IClientAccessTokenProvider accessTokenProvider)
        {
            _accessTokenProvider = accessTokenProvider;
        }

        public ApiCallResponse<T> DownloadJsonDataFromApi<T>(string url)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            ApiCallResponse<T> apiCallResponse = new ApiCallResponse<T>();

            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrWhiteSpace(_accessTokenProvider.AccessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessTokenProvider.AccessToken);
                }

                var result = client.GetAsync(url).Result;

                apiCallResponse.StatusCode = (int)result.StatusCode;
                if (result.IsSuccessStatusCode)
                {
                    apiCallResponse.ResponseObject = JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result);
                }

                return apiCallResponse;
            }
        }
    }
}
