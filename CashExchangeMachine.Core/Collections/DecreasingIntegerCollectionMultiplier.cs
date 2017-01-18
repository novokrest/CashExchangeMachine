using System.Collections.Generic;
using System.Linq;

namespace CashExchangeMachine.Core.Collections
{
    internal class DecreasingIntegerCollectionMultiplier : IDecreasingOrderCollection<int>
    {
        private readonly IDecreasingOrderCollection<int> _innerCollection;
        private readonly int _multiplier;

        public DecreasingIntegerCollectionMultiplier(IDecreasingOrderCollection<int> innerCollection, int multiplier)
        {
            _innerCollection = innerCollection;
            _multiplier = multiplier;
        }

        public IEnumerable<int> DecreasingOrder()
        {
            return _innerCollection.DecreasingOrder().Select(value => value * _multiplier);
        }
    }
}
