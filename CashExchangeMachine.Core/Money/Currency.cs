using System.Collections.Generic;

namespace CashExchangeMachine.Core.Money
{
    public class Currency
    {
        public static Currency Dollar = Currency.Create("Dollar", "$", 100, new[] { 1, 2, 5, 10 }, new[] { 1, 5, 10, 25 });

        private static Currency Create(string name, string symbol, int fractions,
                                      IReadOnlyCollection<int> noteNominals, IReadOnlyCollection<int> coinNominals)
        {
            return new Currency(name, symbol, fractions, noteNominals, coinNominals);
        }

        private Currency(string name, string symbol, int fractions, 
                         IReadOnlyCollection<int> noteNominals, IReadOnlyCollection<int> coinNominals)
        {
            Name = name;
            Symbol = symbol;
            UnitFractions = fractions;
            NoteNominals = noteNominals;
            CoinNominals = coinNominals;
        }

        public string Name { get; }
        public string Symbol { get; }
        public int UnitFractions { get; }
        public IReadOnlyCollection<int> NoteNominals { get; }
        public IReadOnlyCollection<int> CoinNominals { get; }
    }
}
