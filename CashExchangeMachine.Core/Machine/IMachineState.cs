namespace CashExchangeMachine.Core.Machine
{
    internal interface IMachineState
    {
        void InsertNote(int nominal);
        void InsertCoin(int nominal);

        IExchangeResult Exchange();
    }
}
