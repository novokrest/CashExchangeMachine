using System;
using System.Configuration;

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
