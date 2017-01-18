using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashExchangeMachine.Core.Collections
{
    public interface IDecreasingOrderCollection<T>
    {
        IEnumerable<T> DecreasingOrder();
    }
}
