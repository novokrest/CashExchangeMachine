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

        public MoneyInfo(string currency, IReadOnlyCollection<MonetaryAggregateInfo> notes, IReadOnlyCollection<MonetaryAggregateInfo> coins)
        {
            Currency = currency;
            Notes = notes;
            Coins = coins;
        }

        public string Currency { get; }
        public IReadOnlyCollection<MonetaryAggregateInfo> Notes { get; }
        public IReadOnlyCollection<MonetaryAggregateInfo> Coins { get; } 
    }

    public class MonetaryAggregateInfo
    {
        public MonetaryAggregateInfo(int nominal, int count)
        {
            Nominal = nominal;
            Count = count;
        }

        public int Nominal { get; }
        public int Count { get; }
    }

    internal static class CountableCollectoinExtension
    {
        public static IReadOnlyCollection<MonetaryAggregateInfo> ToMonetaryAggregateInfoCollection(this ICountableCollection<int> collection)
        {
            return collection.Select(keyValuePair => new MonetaryAggregateInfo(keyValuePair.Key, keyValuePair.Value))
                             .ToList();
        } 
    }
}