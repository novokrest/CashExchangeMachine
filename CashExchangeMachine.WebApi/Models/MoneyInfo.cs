using System.Collections.Generic;
using System.Linq;
using CashExchangeMachine.Core.Collections;
using CashExchangeMachine.Core.Money;

namespace CashExchangeMachine.WebApi.Models
{
    public class MoneyInfo
    {
        public static MoneyInfo CreateFrom(MoneyCollection money)
        {
            return new MoneyInfo(money.Currency.Name,
                                   money.Notes.ToMonetaryAggregateInfoCollection(),
                                   money.Coins.ToMonetaryAggregateInfoCollection());
        }

        public MoneyInfo()
        {
            
        }

        public MoneyInfo(string currency, IReadOnlyCollection<MonetaryAggregateInfo> notes, IReadOnlyCollection<MonetaryAggregateInfo> coins)
        {
            Currency = currency;
            Notes = notes;
            Coins = coins;
        }

        public string Currency { get; set; }
        public IReadOnlyCollection<MonetaryAggregateInfo> Notes { get; set; }
        public IReadOnlyCollection<MonetaryAggregateInfo> Coins { get; set; } 
    }

    public class MonetaryAggregateInfo
    {
        public MonetaryAggregateInfo()
        {
            
        }

        public MonetaryAggregateInfo(int nominal, int count)
        {
            Nominal = nominal;
            Count = count;
        }

        public int Nominal { get; set; }
        public int Count { get; set; }
    }

    internal static class CountableCollectoinExtension
    {
        public static IReadOnlyCollection<MonetaryAggregateInfo> ToMonetaryAggregateInfoCollection(this RestrictedIntegerCollection collection)
        {
            return collection.Select(keyValuePair => new MonetaryAggregateInfo(keyValuePair.Key, keyValuePair.Value))
                             .ToList();
        } 
    }
}