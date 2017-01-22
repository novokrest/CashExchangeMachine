using CashExchangeMachine.Core.Money;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashExchangeMachine.Core.Money
{
    public interface ICashRepository
    {
        MoneyCollection LoadMoney();
        void AddMoney(MoneyCollection money);
        void RemoveMoney(MoneyCollection money);
    }
}
