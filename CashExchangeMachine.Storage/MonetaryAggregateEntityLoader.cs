using CashExchangeMachine.Core.Money;
using CashExchangeMachine.Storage.Sql;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CashExchangeMachine.Storage
{
    internal abstract class MonetaryAggregateEntityLoader<TMonetaryEntity> 
        where TMonetaryEntity : IMonetaryAggregateEntity
    {
        private const string SelectPattern = "SELECT Nominal, Currency, Count FROM {0} WHERE Currency = '{1}'";

        private readonly ISqlConnectionProvider _sqlConnectionProvider;
        private readonly string _tableName;

        protected MonetaryAggregateEntityLoader(ISqlConnectionProvider sqlConnectionProvider, string tableName)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
            _tableName = tableName;
        }

        public IReadOnlyCollection<TMonetaryEntity> Load(Currency currency)
        {
            return LazyLoad(currency).ToList();
        }

        private IEnumerable<TMonetaryEntity> LazyLoad(Currency currency)
        {
            using (var sqlConnection = _sqlConnectionProvider.OpenSqlConnection())
            using (var sqlCommand = new SqlCommand(string.Format(SelectPattern, _tableName, currency.Name), sqlConnection))
            using (var reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    var monetaryAggregate = CreateEmptyMonetaryAggregate();
                    monetaryAggregate.Nominal = reader.GetInt32(reader.GetOrdinal("Nominal"));
                    monetaryAggregate.Currency = reader.GetString(reader.GetOrdinal("Currency"));
                    monetaryAggregate.Count = reader.GetInt32(reader.GetOrdinal("Count"));

                    yield return monetaryAggregate;
                }
            }
        }

        protected abstract TMonetaryEntity CreateEmptyMonetaryAggregate();

    }
}
