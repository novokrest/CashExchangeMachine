using CashExchangeMachine.Core.Money;

namespace CashExchangeMachine.WebApi.Tests
{
    internal class MoneyBuilder
    {
        private readonly MoneyCollection _money;

        public MoneyBuilder(Currency currency)
        {
            _money = MoneyCollection.Create(currency);
        }

        public MoneyBuilder AddNotes(int nominal, int count = 1)
        {
            _money.Notes.Add(nominal, count);
            return this;
        }

        public MoneyBuilder AddCoins(int nominal, int count = 1)
        {
            _money.Coins.Add(nominal, count);
            return this;
        }

        public MoneyCollection Build()
        {
            return _money;
        }
    }
}
