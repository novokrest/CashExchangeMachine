using CashExchangeMachine.Storage.Sql;

namespace CashExchangeMachine.Storage
{
    internal class CoinRepository : MonetaryAggregateRepository<CoinEntity>
    {
        private const string CoinTableName = "Coins";

        public CoinRepository(ISqlConnectionProvider sqlConnectionProvider) 
            : base(sqlConnectionProvider, CoinTableName)
        {
        }

        protected override CoinEntity CreateEmptyMonetaryAggregate()
        {
            return new CoinEntity();
        }
    }
}
