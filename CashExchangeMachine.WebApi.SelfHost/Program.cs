using System;
using System.Configuration;
using Microsoft.Owin.Hosting;
using CashExchangeMachine.Storage.Sql;

namespace CashExchangeMachine.WebApi.SelfHost
{
    internal static class Program
    {
        private static readonly string ServerUrl = ConfigurationManager.AppSettings["server"];
        private static readonly string SqlConnectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

        public static void Main(string[] args)
        {
            CheckSqlConnection();
            StartServer();
        }

        private static void CheckSqlConnection()
        {
            var sqlConnectionProvider = new SqlConnectionProvider(SqlConnectionString);
            using (sqlConnectionProvider.OpenSqlConnection())
            {
            }
        }

        private static void StartServer()
        {
            using (WebApp.Start<Startup>(url: ServerUrl))
            {
                Console.WriteLine("Server running at {0} - press Enter to quit... ", ServerUrl);
                Console.ReadLine();
            }
        }
    }
}
