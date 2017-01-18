using CashExchangeMachine.Core.Money;
using System;
using System.Reflection;

namespace CashExchangeMachine.Core.Machine.States
{
    internal abstract class MachineStateBase : IMachineState
    {
        private readonly IMachineStateOwner _owner;

        public MachineStateBase(IMachineStateOwner owner, MoneyCollection moneyCollection)
        {
            _owner = owner;
            Money = moneyCollection;
        }

        protected MoneyCollection Money { get; }

        public abstract IExchangeResult Exchange();
        public abstract void InsertCoin(int nominal);
        public abstract void InsertNote(int nominal);

        protected void ChangeOwnerState(IMachineState machineState)
        {
            _owner.ChangeState(machineState);
        }

        protected T ChangeOwnerState<T>() where T : MachineStateBase
        {
            T newState = CreateNewState<T>();
            _owner.ChangeState(newState);
            return newState;
        }

        private T CreateNewState<T>() where T : MachineStateBase
        {
            Type newStateType = typeof(T);
            ConstructorInfo ctor = newStateType.GetConstructor(new[] { typeof(IMachineStateOwner), typeof(MoneyCollection) });
            return (T)ctor.Invoke(new object[] { _owner, Money });
        }
    }
}
