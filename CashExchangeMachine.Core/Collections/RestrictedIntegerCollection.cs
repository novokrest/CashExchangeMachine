using System.Collections;
using System.Collections.Generic;

namespace CashExchangeMachine.Core.Collections
{
    public class RestrictedIntegerCollection : ICountableCollection<int>, IDecreasingOrderCollection<int>
    {
        private readonly RestrictedValueCollection<int> _restrictedValueCollection;
        private int _total;

        public RestrictedIntegerCollection(IEnumerable<int> allowableValues) 
        {
            _restrictedValueCollection = new RestrictedValueCollection<int>(allowableValues);
        }

        public int Total => _total;

        public void Add(int element, int count)
        {
            _restrictedValueCollection.Add(element, count);
            _total += element * count;
        }

        public int Count(int element)
        {
            return _restrictedValueCollection.Count(element);
        }

        public IEnumerable<int> DecreasingOrder()
        {
            return _restrictedValueCollection.DecreasingOrder();
        }

        public void Remove(int element, int count)
        {
            _restrictedValueCollection.Remove(element, count);
            _total -= element * count;
        }

        public IEnumerator<KeyValuePair<int, int>> GetEnumerator()
        {
            return _restrictedValueCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
