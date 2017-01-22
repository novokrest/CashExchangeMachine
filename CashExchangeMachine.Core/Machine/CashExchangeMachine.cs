using CashExchangeMachine.Core.Machine.States;
using CashExchangeMachine.Core.Money;

namespace CashExchangeMachine.Core.Machine
{
    public interface ICashExchangeMachine
    {
        void SetMoney(MoneyCollection money);
        MoneyCollection GetAvailableMoney();

        void InsertNote(int nominal);
        void InsertCoin(int nominal);

        IExchangeResult ConfirmExchange();
    }

    public class CashExchangeMachine : ICashExchangeMachine, IMachineStateOwner
    {
        private IMachineState _state;

        public CashExchangeMachine(ICashRepository cashRepository, Currency currency)
        {
            _state = new FreshMachineState(this, cashRepository, currency);
        }

        void IMachineStateOwner.ChangeState(IMachineState state)
        {
            _state = state;
        }

        public void SetMoney(MoneyCollection money)
        {
            _state.SetMoney(money);
        }

        public MoneyCollection GetAvailableMoney()
        {
            return _state.GetAvailableMoney();
        }

        public void InsertNote(int nominal)
        {
            _state.InsertNote(nominal);
        }

        public void InsertCoin(int nominal)
        {
            _state.InsertCoin(nominal);
        }

        public IExchangeResult ConfirmExchange()
        {
            return _state.Exchange();
        }
    }
}
