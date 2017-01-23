namespace CashExchangeMachine.Core.Money
{
    public class MoneyCollection
    {
        //TODO: MoneyCollection should created from Notes or/and Coins
        //TODO: Notes.Add should accept only Notes, Coins - only Coins
        // TODO: create more friendly API for Notes and Coins class
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
