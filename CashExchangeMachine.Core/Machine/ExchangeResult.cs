using CashExchangeMachine.Core.Money;

namespace CashExchangeMachine.Core.Machine
{
    public interface IExchangeResult
    {
        bool Success { get; }
        MoneyCollection Money { get; }
    }

    //TODO: static methods for ExchangeResult.Success and Failed
    internal class ExchangeResult : IExchangeResult
    {
        public bool Success { get; set; }
        public MoneyCollection Money { get; set; }
    }
}
