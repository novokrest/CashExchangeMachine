using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashExchangeMachine.Storage
{
    internal class NoteEntity : IMonetaryAggregateEntity
    {
        public int Nominal { get; set; }
        public string Currency { get; set; }
        public int Count { get; set; }
    }
}
