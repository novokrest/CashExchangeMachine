using System.Data.SqlClient;

namespace CashExchangeMachine.Storage.Sql
{
    public interface ISqlConnectionProvider
    {
        SqlConnection OpenSqlConnection();
    }

    public class SqlConnectionProvider : ISqlConnectionProvider
    {
        private readonly string _sqlConnectionString;

        public SqlConnectionProvider(string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
        }

        public SqlConnection OpenSqlConnection()
        {
            var sqlConnectionBuilder = new SqlConnectionStringBuilder(_sqlConnectionString)
            {
                Pooling = true
            };

            var sqlConnection = new SqlConnection(sqlConnectionBuilder.ToString());
            sqlConnection.Open();

            return sqlConnection;
        }
    }
}
