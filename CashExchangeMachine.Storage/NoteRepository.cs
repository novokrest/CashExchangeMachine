using CashExchangeMachine.Storage.Sql;

namespace CashExchangeMachine.Storage
{
    internal class NoteRepository : MonetaryAggregateRepository<NoteShift>
    {
        private const string NoteTableName = "Notes";

        public NoteRepository(ISqlConnectionProvider sqlConnectionProvider) 
            : base(sqlConnectionProvider, NoteTableName)
        {
        }

        protected override NoteShift CreateEmptyMonetaryAggregate()
        {
            return new NoteShift();
        }
    }
}
