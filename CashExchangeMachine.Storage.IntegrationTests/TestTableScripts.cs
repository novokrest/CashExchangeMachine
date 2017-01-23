using System.Text;

namespace CashExchangeMachine.Storage.IntegrationTests
{
    internal class TestTableScripts
    {
        private readonly string _tableName;

        public TestTableScripts(string tableName)
        {
            _tableName = tableName;
        }

        public string RemoveScript()
        {
            return $@"IF OBJECT_ID('[dbo].[{_tableName}]', 'U') IS NOT NULL 
                        DROP TABLE [dbo].[{_tableName}]";
        }

        public string CreateScript()
        {
            return $@"CREATE TABLE {_tableName} 
                      (
                        [Nominal] INT NOT NULL,
	                    [Currency] VARCHAR(128) NOT NULL,
	                    [Count] INT NOT NULL,
	                    PRIMARY KEY (Nominal, Currency)
                      )";
        }

        public string InsertScripts(params IMonetaryAggregateShift[] shifts)
        {
            var scriptBuilder = new StringBuilder();
            foreach (var entity in shifts)
            {
                scriptBuilder.AppendLine($@"INSERT INTO {_tableName} VALUES ({entity.Nominal}, '{entity.Currency}', {entity.Count});");
            }
            return scriptBuilder.ToString();
        }
    }
}
