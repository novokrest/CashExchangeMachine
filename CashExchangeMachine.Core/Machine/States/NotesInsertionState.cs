using CashExchangeMachine.Core.Collections;
using CashExchangeMachine.Core.Exchangers;
using CashExchangeMachine.Core.Extensions;
using CashExchangeMachine.Core.Money;
using System;

namespace CashExchangeMachine.Core.Machine.States
{
    internal class NotesInsertionState : MachineStateBase
    {
        private readonly Notes _insertedNotes;

        public NotesInsertionState(IMachineStateOwner owner, MoneyCollection moneyCollection) 
            : base(owner, moneyCollection)
        {
            _insertedNotes = Notes.Create(Money.Currency);
        }

        public override void InsertNote(int nominal)
        {
            _insertedNotes.Add(nominal, 1);
        }

        public override void InsertCoin(int nominal)
        {
            throw new InvalidOperationException("Failed to insert coin! You can insert one more note or confirm exchange.");
        }

        public override IExchangeResult Exchange()
        {
            ICountableCollection<int> exchangeResult;
            int valueForExchange = _insertedNotes.Total * Money.Currency.UnitFractions;

            var greedyExchanger = new GreedyExchanger();
            if (greedyExchanger.TryExchange(Money.Coins, valueForExchange, out exchangeResult))
            {
                var exchangedMoney = MoneyCollection.Create(Money.Currency);
                exchangedMoney.Coins.Add(exchangeResult);

                return new ExchangeResult
                {
                    Success = true,
                    Money = exchangedMoney
                };
            }

            var insertedMoney = MoneyCollection.Create(Money.Currency);
            insertedMoney.Notes.Add(_insertedNotes);

            return new ExchangeResult
            {
                Success = false,
                Money = insertedMoney
            };
        }

        //TODO: Notes and Coins should store Currency
        //TODO: MoneyCollection should created from Notes or/and Coins
        //TODO: static methods for ExchangeResult.Success and Failed
        //TODO: Notes.Add should accept only Notes, Coins - only Coins
    }
}
