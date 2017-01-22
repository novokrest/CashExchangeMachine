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
            Assert.IsTrue(moneyResult.Coins.All(monetaryAggragateInfo => monetaryAggragateInfo.Count == 0));
            return moneyResult;
        }

        public static MoneyResult AssertNoNotes(this MoneyResult moneyResult)
        {
            Assert.IsTrue(moneyResult.Notes.All(monetaryAggragateInfo => monetaryAggragateInfo.Count == 0));
            return moneyResult;
        }
    }
}
