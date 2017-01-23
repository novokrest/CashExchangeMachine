using CashExchangeMachine.Storage.Sql;

namespace CashExchangeMachine.Storage
{
    internal class CoinRepository : MonetaryAggregateRepository<CoinShift>
    {
        private const string CoinTableName = "Coins";

        public CoinRepository(ISqlConnectionProvider sqlConnectionProvider) 
            : base(sqlConnectionProvider, CoinTableName)
        {
        }

        protected override CoinShift CreateEmptyMonetaryAggregate()
        {
            return new CoinShift();
        }
    }
}
