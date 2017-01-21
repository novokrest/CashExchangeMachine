namespace CashExchangeMachine.Storage
{
    internal class CoinEntity : IMonetaryAggregateEntity
    {
        public int Count { get; set; }

        public string Currency { get; set; }

        public int Nominal { get; set; }
    }
}
