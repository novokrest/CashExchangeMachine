using CashExchangeMachine.Core.Collections;

namespace CashExchangeMachine.Core.Exchangers
{
    internal interface IExchanger<T>
    {
        bool TryExchange(IDecreasingOrderCollection<T> availableValues, T valueForExchange, out ICountableCollection<T> result);
    }
}
