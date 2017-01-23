namespace CashExchangeMachine.Core.Money
{
    public interface ICashRepository
    {
        MoneyCollection LoadMoney(Currency currency);
        void AddMoney(MoneyCollection money);
        void RemoveMoney(MoneyCollection money);
        void SetMoney(MoneyCollection money);
    }
}
