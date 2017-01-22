using CashExchangeMachine.Core.Money;
using System;

namespace CashExchangeMachine.Core.Machine.States
{
    internal class FreshMachineState : IMachineState
    {
        private readonly IMachineStateOwner _owner;
        private readonly ICashRepository _cashRepository;
        private readonly Currency _currency;

        public FreshMachineState(IMachineStateOwner owner, ICashRepository cashRepository, Currency currency)
        {
            _owner = owner;
            _cashRepository = cashRepository;
            _currency = currency;
        }

        public MoneyCollection GetAvailableMoney()
        {
            return _cashRepository.LoadMoney(_currency);
        }

        public void SetMoney(MoneyCollection money)
        {
            _cashRepository.SetMoney(money);
        }

        public void InsertNote(int nominal)
        {
            _owner.ChangeState<NotesInsertionState>(_cashRepository, _currency)
                  .InsertNote(nominal);
        }

        public void InsertCoin(int nominal)
        {
            _owner.ChangeState<CoinsInsertionState>(_cashRepository, _currency)
                  .InsertCoin(nominal);
        }

        public IExchangeResult Exchange()
        {
            throw new InvalidOperationException("No money has been inserted");
        }
    }
}
