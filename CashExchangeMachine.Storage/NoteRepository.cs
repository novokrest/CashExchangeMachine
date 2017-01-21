using CashExchangeMachine.Core.Money;
using CashExchangeMachine.Storage.Sql;

namespace CashExchangeMachine.Storage
{
    internal class NoteRepository : MonetaryAggregateRepository<NoteEntity>
    {
        private const string NoteTableName = "Notes";

        public NoteRepository(ISqlConnectionProvider sqlConnectionProvider) 
            : base(sqlConnectionProvider, NoteTableName)
        {
        }

        protected override NoteEntity CreateEmptyMonetaryAggregate()
        {
            return new NoteEntity();
        }
    }
}
