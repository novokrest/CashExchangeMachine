namespace CashExchangeMachine.Storage
{
    internal interface IMonetaryAggregateEntity
    {
        int Nominal { get; set; }
        string Currency { get; set; }
        int Count { get; set; }
    }
}
