using CashExchangeMachine.Core.Money;
using CashExchangeMachine.Storage.Sql;

namespace CashExchangeMachine.Storage
{
    internal class NoteEntityLoader : MonetaryAggregateEntityLoader<NoteEntity>
    {
        private const string NoteTableName = "Notes";

        public NoteEntityLoader(ISqlConnectionProvider sqlConnectionProvider) 
            : base(sqlConnectionProvider, NoteTableName)
        {
        }

        protected override NoteEntity CreateEmptyMonetaryAggregate()
        {
            return new NoteEntity();
        }
    }
}
