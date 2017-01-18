using CashExchangeMachine.Core.Collections;

namespace CashExchangeMachine.Core.Exchangers
{
    internal class GreedyExchanger : IExchanger<int>
    {
        public bool TryExchange(IDecreasingOrderCollection<int> availableValues, int valueForExchange, out ICountableCollection<int> result)
        {
            result = new CountableCollection<int>();
            foreach (int availableValue in availableValues.DecreasingOrder())
            {
                if (valueForExchange == 0) { return true; }
                if (valueForExchange >= availableValue)
                {
                    valueForExchange -= availableValue;
                    result.Add(availableValue, 1);
                }
            }

            return valueForExchange == 0;
        }
    }
}
