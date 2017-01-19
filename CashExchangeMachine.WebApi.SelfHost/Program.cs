using System.Configuration;

namespace CashExchangeMachine.WebApi.SelfHost
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Server.Run(ConfigurationManager.AppSettings["server"]);
        }
    }
}
