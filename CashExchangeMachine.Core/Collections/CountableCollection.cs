using System.Collections.Generic;
using System;
using System.Collections;

namespace CashExchangeMachine.Core.Collections
{
    public interface ICountableCollection<T> : IEnumerable<KeyValuePair<T, int>>
    {
        void Add(T element, int count);
        void Remove(T element, int count);
        int Count(T element);
    }

    public class CountableCollection<T> : ICountableCollection<T>
    {
        private readonly IDictionary<T, int> _elements = new Dictionary<T, int>();

        public void Add(T element, int count)
        {
            if (!_elements.ContainsKey(element))
            {
                _elements.Add(element, 0);
            }
            _elements[element] += count;
        }

        public void Remove(T element, int count)
        {
            Verifiers.Verify(_elements.ContainsKey(element) && _elements[element] >= count, "Not enough elements");

            _elements[element] -= count;
            if (_elements[element] == 0)
            {
                _elements.Remove(element);
            }
        }

        public int Count(T element)
        {
            return _elements.ContainsKey(element) ? _elements[element] : 0;
        }

        public IEnumerator<KeyValuePair<T, int>> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
