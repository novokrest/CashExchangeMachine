using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace CashExchangeMachine.WebApi.SelfHost
{
    internal class Server
    {
        private readonly string _url;

        public static void Run(string url)
        {
            new Server(url).Run();
        }

        private Server(string url)
        {
            _url = url;
        }

        private void Run()
        {
            using (var server = new HttpSelfHostServer(CreateConfiguration()))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("Press Enter to quit...");
                Console.ReadLine();
            }
        }

        private HttpSelfHostConfiguration CreateConfiguration()
        {
            var config = new HttpSelfHostConfiguration(_url);
            WebApiConfig.Register(config);

            return config;
        }
    }
}
