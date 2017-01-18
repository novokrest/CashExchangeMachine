using CashExchangeMachine.Core.Collections;
using System.Collections.Generic;

namespace CashExchangeMachine.Core.Money
{
    public class Coins : RestrictedIntegerCollection
    {
        public static Coins Create(Currency currency)
        {
            return new Coins(currency.CoinNominals);
        }

        private Coins(IEnumerable<int> nominals)
            : base(nominals)
        {

        }
    }
}
