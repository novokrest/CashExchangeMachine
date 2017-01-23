namespace CashExchangeMachine.Storage
{
    internal interface IMonetaryAggregateShift
    {
        int Nominal { get; set; }
        string Currency { get; set; }
        int Count { get; set; }
    }
}
