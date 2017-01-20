using System;
using System.Configuration;
using Microsoft.Owin.Hosting;

namespace CashExchangeMachine.WebApi.SelfHost
{
    internal static class Program
    {
        private static readonly string ServerUrl = ConfigurationManager.AppSettings["server"];

        public static void Main(string[] args)
        {
            using (WebApp.Start<Startup>(url: ServerUrl))
            {
                Console.WriteLine("Server running at {0} - press Enter to quit. ", ServerUrl);
                Console.WriteLine("Press Enter to quit...");
                Console.ReadLine();
            }
        }
    }
}
