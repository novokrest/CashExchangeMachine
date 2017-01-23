using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CashExchangeMachine.Core.Money;

namespace CashExchangeMachine.WebApi.Tests
{
    internal class MoneyCollectionEqualityComparer : IEqualityComparer<MoneyCollection>
    {
        public static bool IsEquals(MoneyCollection x, MoneyCollection y)
        {
            return new MoneyCollectionEqualityComparer().Equals(x, y);
        }

        public bool Equals(MoneyCollection x, MoneyCollection y)
        {
            return x.Notes.DecreasingOrder().SequenceEqual(y.Notes.DecreasingOrder())
                && x.Coins.DecreasingOrder().SequenceEqual(y.Coins.DecreasingOrder());
        }

        public int GetHashCode(MoneyCollection obj)
        {
            return obj.Notes.Total * 7 + obj.Coins.Total;
        }
    }
}
