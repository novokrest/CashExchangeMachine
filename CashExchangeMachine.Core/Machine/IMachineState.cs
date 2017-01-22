using CashExchangeMachine.Core.Money;

namespace CashExchangeMachine.Core.Machine
{
    internal interface IMachineState
    {
        void SetMoney(MoneyCollection money);
        MoneyCollection GetAvailableMoney();

        void InsertNote(int nominal);
        void InsertCoin(int nominal);

        IExchangeResult Exchange();
    }
}
