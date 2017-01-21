using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashExchangeMachine.Storage
{
    internal interface IMonetaryAggregateEntity
    {
        int Nominal { get; set; }
        string Currency { get; set; }
        int Count { get; set; }
    }
}
