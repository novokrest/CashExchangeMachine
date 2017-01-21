using CashExchangeMachine.Storage.Sql;

namespace CashExchangeMachine.Storage
{
    internal class CoinEntityLoader : MonetaryAggregateEntityLoader<CoinEntity>
    {
        private const string CoinTableName = "Coins";

        public CoinEntityLoader(ISqlConnectionProvider sqlConnectionProvider) 
            : base(sqlConnectionProvider, CoinTableName)
        {
        }

        protected override CoinEntity CreateEmptyMonetaryAggregate()
        {
            return new CoinEntity();
        }
    }
}
