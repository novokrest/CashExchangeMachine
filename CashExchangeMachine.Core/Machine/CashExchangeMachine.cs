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
        private readonly object _lock = new object();
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
            lock (_lock)
            {
                _state.SetMoney(money);
            }
        }

        public MoneyCollection GetAvailableMoney()
        {
            lock (_lock)
            {
                return _state.GetAvailableMoney();
            }
        }

        public void InsertNote(int nominal)
        {
            lock (_lock)
            {
                _state.InsertNote(nominal);
            }
            
        }

        public void InsertCoin(int nominal)
        {
            lock (_lock)
            {
                _state.InsertCoin(nominal);
            }
        }

        public IExchangeResult ConfirmExchange()
        {
            lock (_lock)
            {
                return _state.Exchange();
            }
        }
    }
}
