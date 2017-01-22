using CashExchangeMachine.Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;

namespace CashExchangeMachine.Core.Collections
{
    public class RestrictedValueCollection<T> : ICountableCollection<T>, IDecreasingOrderCollection<T>
    {
        private readonly ICountableCollection<T> _valueCollection = new CountableCollection<T>();
        private readonly ISet<T> _allowableValues;

        public RestrictedValueCollection(IEnumerable<T> allowableValues)
        {
            _allowableValues = allowableValues.ToSet();
        }

        public IEnumerable<T> DecreasingOrder()
        {
            return _allowableValues.InDescreaseOrder()
                                   .SelectMany(value => Enumerable.Repeat(value, _valueCollection.Count(value)));
        }

        public void Add(T value, int count)
        {
            CheckValue(value);
            _valueCollection.Add(value, count);
        }

        public void Remove(T value, int count)
        {
            CheckValue(value);
            _valueCollection.Remove(value, count);
        }

        public int Count(T value)
        {
            CheckValue(value);
            return _valueCollection.Count(value);
        }

        private void CheckValue(T value)
        {
            Verifiers.Verify(_allowableValues.Contains(value), "Incorrect value: {0}", value);
        }

        public IEnumerator<KeyValuePair<T, int>> GetEnumerator()
        {
            return _allowableValues.Select(value => 
                                           new KeyValuePair<T, int>(value, _valueCollection.Count(value)))
                                   .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
