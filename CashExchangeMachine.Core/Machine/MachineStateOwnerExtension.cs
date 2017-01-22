using System;
using System.Linq;
using System.Reflection;

namespace CashExchangeMachine.Core.Machine
{
    internal static class MachineStateOwnerExtension
    {
        public static TMachineState ChangeState<TMachineState>(this IMachineStateOwner owner, params object[] args) 
            where TMachineState : IMachineState
        {
            TMachineState newState = CreateNewState<TMachineState>(owner, args);
            owner.ChangeState(newState);
            return newState;
        }

        private static TMachineState CreateNewState<TMachineState>(IMachineStateOwner owner, params object[] args) 
            where TMachineState : IMachineState
        {
            Type newStateType = typeof(TMachineState);
            object[] ctorArgs = new[] {owner}.Concat(args).ToArray();
            Type[] ctorArgTypes = ctorArgs.Select(arg => arg.GetType()).ToArray();

            ConstructorInfo ctor = newStateType.GetConstructor(ctorArgTypes);
            Verifiers.Assert(ctor != null, "No constructor with specified signature has been found: {0}", (object)(ctorArgTypes));

            return (TMachineState) ctor.Invoke(ctorArgs);
        }
    }
}
