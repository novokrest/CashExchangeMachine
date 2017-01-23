namespace CashExchangeMachine.Storage
{
    internal class CoinShift : IMonetaryAggregateShift
    {
        public int Count { get; set; }

        public string Currency { get; set; }

        public int Nominal { get; set; }
    }
}
