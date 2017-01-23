using CashExchangeMachine.Core;
using System;
using System.Net.Http;

namespace CashExchangeMachine.WebApi.Client
{
    internal sealed class WebClient : IDisposable
    {
        private readonly HttpClient _httpClient;

        public static WebClient Create(string server)
        {
            HttpClient client = null;
            try
            {
                client = new HttpClient { BaseAddress = new Uri(server) };
                HttpClient tmp = client; client = null;
                return new WebClient(tmp);
            }
            finally
            {
                client?.Dispose();
            }
        }

        private WebClient(HttpClient httpClient)
        {
            Verifiers.ArgNullVerify(httpClient, nameof(httpClient));
            _httpClient = httpClient;
        }

        public string GetAllTestObjects()
        {
            return GetString("api/test");
        }

        private string GetString(string path)
        {
            var response = _httpClient.GetAsync(path).Result;
            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsStringAsync().Result;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
