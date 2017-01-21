using CashExchangeMachine.Core.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CashExchangeMachine.Core.Money
{
    public class Notes : RestrictedIntegerCollection
    {
        public static Notes Create(Currency currency, IEnumerable<Tuple<int, int>> noteNominalCountPairs = null)
        {
            noteNominalCountPairs = noteNominalCountPairs ?? Enumerable.Empty<Tuple<int, int>>();

            var notes = new Notes(currency.NoteNominals);
            foreach (var noteNominalCountPair in noteNominalCountPairs)
            {
                notes.Add(noteNominalCountPair.Item1, noteNominalCountPair.Item2);
            }

            return notes;
        }

        private Notes(IEnumerable<int> nominals)
            : base(nominals)
        {

        }
    }
}
