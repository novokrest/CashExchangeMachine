using CashExchangeMachine.Core;
using CashExchangeMachine.Core.Money;
using CashExchangeMachine.Storage.Sql;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CashExchangeMachine.Storage
{
    internal abstract class MonetaryAggregateRepository<TMonetaryAggregateEntity> 
        where TMonetaryAggregateEntity : IMonetaryAggregateEntity
    {
        // TODO: create method for returning select query
        private const string SelectPattern = "SELECT Nominal, Currency, Count FROM {0} WHERE Currency = '{1}'";

        private readonly ISqlConnectionProvider _sqlConnectionProvider;
        private readonly string _tableName;

        protected MonetaryAggregateRepository(ISqlConnectionProvider sqlConnectionProvider, string tableName)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
            _tableName = tableName;
        }

        public IReadOnlyCollection<TMonetaryAggregateEntity> Load(Currency currency)
        {
            return LazyLoad(currency).ToList();
        }

        private IEnumerable<TMonetaryAggregateEntity> LazyLoad(Currency currency)
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

        public void Insert(TMonetaryAggregateEntity entity)
        {
            using (var sqlConnection = _sqlConnectionProvider.OpenSqlConnection())
            using (var sqlCommand = new SqlCommand(CreateInsertQuery(entity), sqlConnection))
            {
                int rowsAffected = sqlCommand.ExecuteNonQuery();
                Verifiers.Verify(rowsAffected == 1, "Money insertion failed");
            }
        }

        private string CreateInsertQuery(TMonetaryAggregateEntity entity)
        {
            return $@"UPDATE {_tableName} SET [Count] = [Count] + {entity.Count} 
                                          WHERE [Nominal] = {entity.Nominal} AND Currency = '{entity.Currency}'";
        }


        // TODO: MonetaryAggregateEntity -> MonetaryAggregateDiff, choose more evident names for methods
        public void Delete(TMonetaryAggregateEntity entity)
        {
            using (var connection = _sqlConnectionProvider.OpenSqlConnection())
            using (var command = new SqlCommand(CreateDeleteQuery(entity), connection))
            {
                int rowsAffected = command.ExecuteNonQuery();
                Verifiers.Verify(rowsAffected == 1, "Money deletion failed");
            }
        }

        private string CreateDeleteQuery(TMonetaryAggregateEntity entity)
        {
            return $@"UPDATE {_tableName} SET [Count] = [Count] - {entity.Count}
                      WHERE [Nominal] = {entity.Nominal} AND Currency = '{entity.Currency}'";
        }

        protected abstract TMonetaryAggregateEntity CreateEmptyMonetaryAggregate();

    }
}
