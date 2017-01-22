using CashExchangeMachine.Core.Machine.States;
using CashExchangeMachine.Core.Money;

namespace CashExchangeMachine.Core.Machine
{
    public interface ICashExchangeMachine
    {
        MoneyCollection Money { get; }
        void InsertNote(int nominal);
        void InsertCoin(int nominal);

        IExchangeResult ConfirmExchange();
    }

    internal class CashExchangeMachine : ICashExchangeMachine, IMachineStateOwner
    {
        private readonly ICashRepository _cashRepository;
        private IMachineState _state;

        public CashExchangeMachine(ICashRepository cashRepository)
        {
            _cashRepository = cashRepository;
            _state = new FreshMachineState(this, Money);
        }

        public MoneyCollection Money => _cashRepository.LoadMoney();

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
