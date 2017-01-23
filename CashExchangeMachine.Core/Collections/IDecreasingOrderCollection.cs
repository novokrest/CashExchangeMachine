using System.Collections.Generic;

namespace CashExchangeMachine.Core.Collections
{
    public interface IDecreasingOrderCollection<T>
    {
        IEnumerable<T> DecreasingOrder();
    }
}
