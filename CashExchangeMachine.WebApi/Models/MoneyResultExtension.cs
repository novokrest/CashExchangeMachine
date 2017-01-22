using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CashExchangeMachine.Core.Extensions;
using CashExchangeMachine.Core.Money;

namespace CashExchangeMachine.WebApi.Models
{
    public static class MoneyResultExtension
    {
        public static MoneyCollection ToMoneyCollection(this MoneyResult moneyResult)
        {
            var moneyCollection = MoneyCollection.Create(CurrencyRegistry.GetCurrency(moneyResult.Currency));
            moneyResult.Coins.ForEach(coinInfo => moneyCollection.Coins.Add(coinInfo.Nominal, coinInfo.Count));
            moneyResult.Notes.ForEach(noteInfo => moneyCollection.Notes.Add(noteInfo.Nominal, noteInfo.Count));

            return moneyCollection;
        }
    }
}