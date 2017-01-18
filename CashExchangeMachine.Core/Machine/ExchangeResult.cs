using CashExchangeMachine.Core.Money;

namespace CashExchangeMachine.Core.Machine
{
    public interface IExchangeResult
    {
        bool Success { get; }
        MoneyCollection Money { get; }
    }

    internal class ExchangeResult : IExchangeResult
    {
        public bool Success { get; set; }
        public MoneyCollection Money { get; set; }
    }
}
