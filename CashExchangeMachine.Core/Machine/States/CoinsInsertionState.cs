using CashExchangeMachine.Core.Collections;
using CashExchangeMachine.Core.Exchangers;
using CashExchangeMachine.Core.Extensions;
using CashExchangeMachine.Core.Money;
using System;

namespace CashExchangeMachine.Core.Machine.States
{
    internal class CoinsInsertionState : MachineStateBase
    {
        private readonly Coins _insertedCoins; 

        public CoinsInsertionState(IMachineStateOwner owner, MoneyCollection moneyCollection) 
            : base(owner, moneyCollection)
        {
            _insertedCoins = Coins.Create(Money.Currency);
        }

        public override void InsertNote(int nominal)
        {
            throw new InvalidOperationException("Failed to insert note! You can insert one more coin or confirm exchange.");
        }

        public override void InsertCoin(int nominal)
        {
            _insertedCoins.Add(nominal, 1);
        }

        public override IExchangeResult Exchange()
        {
            int valueForExchange = _insertedCoins.Total;
            ICountableCollection<int> exchangeResult = null;

            var greedyExchanger = new GreedyExchanger();
            if (greedyExchanger.TryExchange(new DecreasingIntegerCollectionMultiplier(Money.Notes, Money.Currency.UnitFractions), 
                                            valueForExchange, out exchangeResult))
            {
                var exchangedMoney = MoneyCollection.Create(Money.Currency);
                exchangedMoney.Notes.Add(exchangeResult.Select(nominal => nominal / Money.Currency.UnitFractions));

                return new ExchangeResult
                {
                    Success = true,
                    Money = exchangedMoney
                };
            }

            var insertedMoney = MoneyCollection.Create(Money.Currency);
            insertedMoney.Coins.Add(_insertedCoins);

            return new ExchangeResult
            {
                Success = false,
                Money = insertedMoney
            };
        }
    }
}
