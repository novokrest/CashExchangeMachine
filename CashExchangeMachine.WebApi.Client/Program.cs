using CashExchangeMachine.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CashExchangeMachine.WebApi.Client
{
    internal static class Program
    {
        private static readonly string Server = ConfigurationManager.AppSettings["server"];

        public static void Main(string[] args)
        {
            using (var webClient = WebClient.Create(Server))
            {
                Console.WriteLine(webClient.GetAllTestObjects());
            }
        }
    }
}
