namespace CashExchangeMachine.Core.Money
{
    public class MoneyCollection
    {
        public static MoneyCollection Create(Currency currency)
        {
            return new MoneyCollection(currency);
        }

        private MoneyCollection(Currency currency)
        {
            Verifiers.ArgNullVerify(currency, nameof(currency));

            Notes = Notes.Create(currency);
            Coins = Coins.Create(currency);
            Currency = currency;
        }

        public Notes Notes { get; }
        public Coins Coins { get; }
        public Currency Currency { get; }
    }
}
