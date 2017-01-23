using CashExchangeMachine.Core.Collections;
using CashExchangeMachine.Core.Exchangers;
using CashExchangeMachine.Core.Extensions;
using CashExchangeMachine.Core.Money;
using System;

namespace CashExchangeMachine.Core.Machine.States
{
    internal class CoinsInsertionState : IMachineState
    {
        private readonly IMachineStateOwner _owner;
        private readonly ICashRepository _cashRepository;
        private readonly Currency _currency;

        private readonly Coins _insertedCoins;

        public CoinsInsertionState(IMachineStateOwner owner, ICashRepository cashRepository, Currency currency)
        {
            _owner = owner;
            _cashRepository = cashRepository;
            _currency = currency;
            _insertedCoins = Coins.Create(currency);
        }

        private MoneyCollection Money => _cashRepository.LoadMoney(_currency);

        public void SetMoney(MoneyCollection money)
        {
            throw new InvalidOperationException("Failed to set available money: complete coins exchange");
        }

        public MoneyCollection GetAvailableMoney()
        {
            throw new InvalidOperationException("Failed to get available money: complete notes exchange");
        }

        public void InsertNote(int nominal)
        {
            throw new InvalidOperationException(
                "Failed to insert note! You can insert one more coin or confirm exchange.");
        }

        public void InsertCoin(int nominal)
        {
            _insertedCoins.Add(nominal, 1);
        }

        public IExchangeResult Exchange()
        {
            var resultMoney = MoneyCollection.Create(_currency);
            bool success = TryExchange(resultMoney);

            if (success)
            {
                _cashRepository.RemoveMoney(resultMoney);
            }

            _owner.ChangeState<FreshMachineState>(_cashRepository, _currency);

            return new ExchangeResult
            {
                Success = success,
                Money = resultMoney
            };
        }

        private bool TryExchange(MoneyCollection resultMoney)
        {
            ICountableCollection<int> exchangeResult = null;
            var greedyExchanger = new GreedyExchanger();

            if (greedyExchanger.TryExchange(new DecreasingIntegerCollectionMultiplier(Money.Notes, _currency.UnitFractions),
                                            _insertedCoins.Total, out exchangeResult))
            {
                resultMoney.Notes.Add(exchangeResult.Select(nominal => nominal/Money.Currency.UnitFractions));
                return true;
            }

            resultMoney.Coins.Add(_insertedCoins);
            return false;
        }

    }
}
