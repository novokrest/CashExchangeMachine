using CashExchangeMachine.Core.Collections;
using CashExchangeMachine.Core.Exchangers;
using CashExchangeMachine.Core.Extensions;
using CashExchangeMachine.Core.Money;
using System;

namespace CashExchangeMachine.Core.Machine.States
{
    internal class NotesInsertionState : IMachineState
    {
        private readonly IMachineStateOwner _owner;
        private readonly ICashRepository _cashRepository;
        private readonly Currency _currency;
        private readonly Notes _insertedNotes;

        public NotesInsertionState(IMachineStateOwner owner, ICashRepository cashRepository, Currency currency)
        {
            _owner = owner;
            _cashRepository = cashRepository;
            _currency = currency;
            _insertedNotes = Notes.Create(currency);
        }

        public void SetMoney(MoneyCollection money)
        {
            throw new InvalidOperationException("Failed to set available money: complete notes exchange");
        }

        public MoneyCollection GetAvailableMoney()
        {
            throw new InvalidOperationException("Failed to get available money: complete notes exchange");
        }

        public void InsertNote(int nominal)
        {
            _insertedNotes.Add(nominal, 1);
        }

        public void InsertCoin(int nominal)
        {
            throw new InvalidOperationException("Failed to insert coin! You can insert one more note or confirm exchange.");
        }

        public IExchangeResult Exchange()
        {
            var returnedMoney = MoneyCollection.Create(_currency);
            bool success = TryExchange(returnedMoney);

            if (success)
            {
                _cashRepository.RemoveMoney(returnedMoney);
            }

            _owner.ChangeState<FreshMachineState>(_cashRepository, _currency);

            return new ExchangeResult
            {
                Success = success,
                Money = returnedMoney
            };
        }

        private bool TryExchange(MoneyCollection resultMoney)
        {
            int valueForExchange = _insertedNotes.Total * _currency.UnitFractions;
            var money = _cashRepository.LoadMoney(_currency);

            ICountableCollection<int> exchangeResult;
            var greedyExchanger = new GreedyExchanger();
            if (greedyExchanger.TryExchange(money.Coins, valueForExchange, out exchangeResult))
            {
                resultMoney.Coins.Add(exchangeResult);
                return true;
            }

            resultMoney.Notes.Add(_insertedNotes);
            return false;
        }

        //TODO: Notes and Coins should store Currency
        //TODO: MoneyCollection should created from Notes or/and Coins
        //TODO: static methods for ExchangeResult.Success and Failed
        //TODO: Notes.Add should accept only Notes, Coins - only Coins
        //TODO: Move common logic to base class
    }
}
