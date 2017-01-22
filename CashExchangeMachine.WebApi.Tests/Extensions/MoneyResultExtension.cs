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
        public static MoneyResult AssertNoCoins(this MoneyResult moneyResult)
        {
            Assert.IsTrue(moneyResult.Coins.All(coinInfo => coinInfo.Count == 0));
            return moneyResult;
        }

        public static MoneyResult AssertNoNotes(this MoneyResult moneyResult)
        {
            Assert.IsTrue(moneyResult.Notes.All(noteInfo => noteInfo.Count == 0));
            return moneyResult;
        }

        public static MoneyResult AssertHasNotes(this MoneyResult moneyResult, int nominal, int count)
        {
            Assert.IsTrue(moneyResult.Notes
                                     .Single(noteInfo => noteInfo.Nominal == nominal)
                                     .Count == count);
            return moneyResult;
        }

        public static MoneyResult AssertHasCoins(this MoneyResult moneyResult, int nominal, int count)
        {
            Assert.IsTrue(moneyResult.Coins
                                     .Single(coinInfo => coinInfo.Nominal == nominal)
                                     .Count == count);
            return moneyResult;
        }
    }
}
