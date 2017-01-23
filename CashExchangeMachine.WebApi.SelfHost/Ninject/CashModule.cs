using System.Configuration;
using CashExchangeMachine.Core.Machine;
using CashExchangeMachine.Core.Money;
using CashExchangeMachine.Storage.Sql;
using Ninject.Modules;

namespace CashExchangeMachine.WebApi.SelfHost.Ninject
{
    public class CashModule : NinjectModule
    {
        public override void Load()
        {
            Bind<Currency>().ToConstant(Currency.Dollar);
            Bind<ISqlConnectionProvider>().To<SqlConnectionProvider>()
                                          .WithConstructorArgument("sqlConnectionString", 
                                                                   ConfigurationManager.ConnectionStrings["sql"].ConnectionString);
            Bind<ICashRepository>().To<SqlCashRepository>();
            Bind<ICashExchangeMachine>().To<Core.Machine.CashExchangeMachine>().InSingletonScope();
        }
    }
}
