using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.WebSockets;
using CashExchangeMachine.WebApi.SelfHost;
using Microsoft.Owin.Hosting;
using NUnit.Framework;

namespace CashExchangeMachine.WebApi.Tests
{
    [TestFixture]
    internal abstract class ControllerBaseTest : IDisposable
    {
        protected const string JsonMimeType = "application/json";

        private string _serverUrl;
        private IDisposable _app;

        [OneTimeSetUp]
        public void SetUpHttpServer()
        {
            int port = GetFreeTcpPort();
            _serverUrl = $@"http://localhost:{port}/";

            _app = WebApp.Start<Startup>(_serverUrl);
            HttpClient = new HttpClient { BaseAddress = new Uri(_serverUrl) };
        }

        protected HttpClient HttpClient { get; private set; }

        private int GetFreeTcpPort()
        {
            using (var listener = new AutoTcpListener(IPAddress.Loopback, 0))
            {
                return listener.Port;
            }
        }

        protected HttpServer HttpServer { get; private set; }

        protected HttpRequestMessage CreateRequest(string url, HttpMethod method)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(_serverUrl + url)
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonMimeType));
            request.Method = method;

            return request;
        }

        protected HttpRequestMessage CreateRequest<T>(string url, HttpMethod method, T body)
        {
            var request = CreateRequest(url, method);

            request.Content = new ObjectContent(typeof(T), body, new JsonMediaTypeFormatter());

            return request;
        }

        public void Dispose()
        {
            _app?.Dispose();
        }
    }
}
