using CashExchangeMachine.Core;
using CashExchangeMachine.Core.Money;
using CashExchangeMachine.Storage.Sql;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CashExchangeMachine.Storage
{
    //TODO: choose more evident names for methods
    internal abstract class MonetaryAggregateRepository<TMonetaryAggregateShift> 
        where TMonetaryAggregateShift : IMonetaryAggregateShift
    {
        private readonly ISqlConnectionProvider _sqlConnectionProvider;
        private readonly string _tableName;

        protected MonetaryAggregateRepository(ISqlConnectionProvider sqlConnectionProvider, string tableName)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
            _tableName = tableName;
        }

        public IReadOnlyCollection<TMonetaryAggregateShift> Load(Currency currency)
        {
            return LazyLoad(currency).ToList();
        }

        private IEnumerable<TMonetaryAggregateShift> LazyLoad(Currency currency)
        {
            using (var sqlConnection = _sqlConnectionProvider.OpenSqlConnection())
            using (var sqlCommand = new SqlCommand(CreateSelectQuery(currency), sqlConnection))
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

        private string CreateSelectQuery(Currency currency)
        {
            return $@"SELECT Nominal, Currency, Count FROM {_tableName} WHERE Currency = '{currency.Name}'";
        }

        public void Insert(TMonetaryAggregateShift entity)
        {
            using (var sqlConnection = _sqlConnectionProvider.OpenSqlConnection())
            using (var sqlCommand = new SqlCommand(CreateInsertQuery(entity), sqlConnection))
            {
                int rowsAffected = sqlCommand.ExecuteNonQuery();
                Verifiers.Verify(rowsAffected == 1, "Money insertion failed");
            }
        }

        private string CreateInsertQuery(TMonetaryAggregateShift entity)
        {
            return $@"UPDATE {_tableName} SET [Count] = [Count] + {entity.Count} 
                                          WHERE [Nominal] = {entity.Nominal} AND Currency = '{entity.Currency}'";
        }

        public void Delete(TMonetaryAggregateShift entity)
        {
            using (var connection = _sqlConnectionProvider.OpenSqlConnection())
            using (var command = new SqlCommand(CreateDeleteQuery(entity), connection))
            {
                int rowsAffected = command.ExecuteNonQuery();
                Verifiers.Verify(rowsAffected == 1, "Money deletion failed");
            }
        }

        private string CreateDeleteQuery(TMonetaryAggregateShift entity)
        {
            return $@"UPDATE {_tableName} SET [Count] = [Count] - {entity.Count}
                      WHERE [Nominal] = {entity.Nominal} AND Currency = '{entity.Currency}'";
        }

        protected abstract TMonetaryAggregateShift CreateEmptyMonetaryAggregate();

    }
}
