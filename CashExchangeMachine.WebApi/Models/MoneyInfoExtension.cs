using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CashExchangeMachine.Core.Extensions;
using CashExchangeMachine.Core.Money;

namespace CashExchangeMachine.WebApi.Models
{
    public static class MoneyInfoExtension
    {
        public static MoneyCollection ToMoneyCollection(this MoneyInfo moneyInfo)
        {
            var moneyCollection = MoneyCollection.Create(CurrencyRegistry.GetCurrency(moneyInfo.Currency));
            moneyInfo.Coins.ForEach(coinInfo => moneyCollection.Coins.Add(coinInfo.Nominal, coinInfo.Count));
            moneyInfo.Notes.ForEach(noteInfo => moneyCollection.Notes.Add(noteInfo.Nominal, noteInfo.Count));

            return moneyCollection;
        }
    }
}