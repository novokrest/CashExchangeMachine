using CashExchangeMachine.Core.Money;
using System;

namespace CashExchangeMachine.Core.Machine.States
{
    internal class FreshMachineState : MachineStateBase
    {
        public FreshMachineState(IMachineStateOwner owner, MoneyCollection money) 
            : base(owner, money)
        {
        }

        public override void InsertNote(int nominal)
        {
            ChangeOwnerState<NotesInsertionState>().InsertNote(nominal);
        }

        public override void InsertCoin(int nominal)
        {
            ChangeOwnerState<CoinsInsertionState>().InsertCoin(nominal);
        }

        public override IExchangeResult Exchange()
        {
            throw new InvalidOperationException("No money has been inserted");
        }
    }
}
