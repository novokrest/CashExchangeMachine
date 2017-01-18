using CashExchangeMachine.Core.Machine.States;
using CashExchangeMachine.Core.Money;

namespace CashExchangeMachine.Core.Machine
{
    public interface ICashExchangeMachine
    {
        void InsertNote(int nominal);
        void InsertCoin(int nominal);

        IExchangeResult ConfirmExchange();
    }

    internal class CashExchangeMachine : ICashExchangeMachine, IMachineStateOwner
    {
        public readonly MoneyCollection _moneyCollection;
        public IMachineState _state;

        public CashExchangeMachine(Currency currency)
            : this(MoneyCollection.Create(currency))
        {
        }

        public CashExchangeMachine(MoneyCollection moneyCollection)
        {
            _moneyCollection = moneyCollection;
            _state = new FreshMachineState(this, _moneyCollection);
        }

        public void ChangeState(IMachineState state)
        {
            _state = state;
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
