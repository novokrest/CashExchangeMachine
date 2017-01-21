using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashExchangeMachine.Core.Money
{
    public class CurrencyRegistry
    {
        private static readonly CurrencyRegistry Registry = new CurrencyRegistry();

        static CurrencyRegistry()
        {
            RegisterCurrency(Currency.Dollar);
        }

        public static void RegisterCurrency(Currency currency)
        {
            Registry.RegisterCurrency(currency.Name, currency);
        }

        public static Currency GetCurrency(string currencyName)
        {
            return Registry[currencyName];
        }

        private readonly IDictionary<string, Currency> _currencyByName = new Dictionary<string, Currency>();

        private Currency this[string name] => _currencyByName[name];

        private void RegisterCurrency(string name, Currency currency)
        {
            Verifiers.Verify(!_currencyByName.ContainsKey(currency.Name), "Currency {0} has been already registered");
            _currencyByName.Add(currency.Name, currency);
        }
    }
}
