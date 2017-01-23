namespace CashExchangeMachine.Storage
{
    internal class NoteShift : IMonetaryAggregateShift
    {
        public int Nominal { get; set; }
        public string Currency { get; set; }
        public int Count { get; set; }
    }
}
