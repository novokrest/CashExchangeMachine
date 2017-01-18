using CashExchangeMachine.Core.Collections;
using System.Collections.Generic;

namespace CashExchangeMachine.Core.Money
{
    public class Notes : RestrictedIntegerCollection
    {
        public static Notes Create(Currency currency)
        {
            return new Notes(currency.NoteNominals);
        }

        private Notes(IEnumerable<int> nominals)
            : base(nominals)
        {

        }
    }
}
