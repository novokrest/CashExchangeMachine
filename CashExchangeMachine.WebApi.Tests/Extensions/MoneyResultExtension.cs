using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashExchangeMachine.WebApi.Models;
using NUnit.Framework;

namespace CashExchangeMachine.WebApi.Tests.Extensions
{
    internal static class MoneyResultExtension
    {
        public static MoneyInfo AssertNoCoins(this MoneyInfo moneyInfo)
        {
            Assert.IsTrue(moneyInfo.Coins.All(coinInfo => coinInfo.Count == 0));
            return moneyInfo;
        }

        public static MoneyInfo AssertNoNotes(this MoneyInfo moneyInfo)
        {
            Assert.IsTrue(moneyInfo.Notes.All(noteInfo => noteInfo.Count == 0));
            return moneyInfo;
        }

        public static MoneyInfo AssertHasNotes(this MoneyInfo moneyInfo, int nominal, int count)
        {
            Assert.IsTrue(moneyInfo.Notes
                                     .Single(noteInfo => noteInfo.Nominal == nominal)
                                     .Count == count);
            return moneyInfo;
        }

        public static MoneyInfo AssertHasCoins(this MoneyInfo moneyInfo, int nominal, int count)
        {
            Assert.IsTrue(moneyInfo.Coins
                                     .Single(coinInfo => coinInfo.Nominal == nominal)
                                     .Count == count);
            return moneyInfo;
        }
    }
}
