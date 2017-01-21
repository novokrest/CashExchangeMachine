using CashExchangeMachine.Storage.Sql;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CashExchangeMachine.Storage.IntegrationTests
{
    internal class TestTableInitializer
    {
        private readonly ISqlConnectionProvider _sqlConnectionProvider;
        private readonly TestTableScripts _scripts;

        public static TestTableInitializer Create(string connectionString, string tableName)
        {
            var sqlConnectionProvider = new SqlConnectionProvider(connectionString);
            var scripts = new TestTableScripts(tableName);

            return new TestTableInitializer(sqlConnectionProvider, scripts);
        }

        public TestTableInitializer(ISqlConnectionProvider sqlConnectionProvider, TestTableScripts scripts)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
            _scripts = scripts;
        }

        public void Initialize(IEnumerable<IMonetaryAggregateEntity> entities)
        {
            Run(_scripts.RemoveScript());
            Run(_scripts.CreateScript());
            Run(_scripts.InsertScripts(entities.ToArray()));
        }

        private void Run(string script)
        {
            using (var connection = _sqlConnectionProvider.OpenSqlConnection())
            using (var command = new SqlCommand(script, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
