namespace CashExchangeMachine.Core.Machine
{
    internal interface IMachineStateOwner
    {
        void ChangeState(IMachineState machineState);
    }
}
