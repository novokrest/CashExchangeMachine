using CashExchangeMachine.Core.Money;
using System;

namespace CashExchangeMachine.Storage.Sql
{
    public class SqlCashRepository : ICashRepository
    {
        private readonly ISqlConnectionProvider _sqlConnectionProvider;
        private readonly Currency _currency;

        private MoneyCollection _moneyCollection;
        
        public SqlCashRepository(ISqlConnectionProvider sqlConnectionProvider, Currency currency)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
            _currency = currency;
        }

        public MoneyCollection LoadMoney()
        {
            EnsureMoneyLoaded();
            return _moneyCollection;
        }

        private void EnsureMoneyLoaded()
        {
            if (_moneyCollection != null) { return; }

            var moneyCollection = MoneyCollection.Create(_currency);
            LoadNotes(moneyCollection);
            LoadCoins(moneyCollection);

            _moneyCollection = moneyCollection;
        }

        private void LoadNotes(MoneyCollection moneyCollection)
        {
            var noteLoader = new NoteEntityLoader(_sqlConnectionProvider);
            foreach (var noteEntity in noteLoader.Load(moneyCollection.Currency))
            {
                moneyCollection.Notes.Add(noteEntity.Nominal, noteEntity.Count);
            }            
        }

        private void LoadCoins(MoneyCollection moneyCollection)
        {
            var coinLoader = new CoinEntityLoader(_sqlConnectionProvider);
            foreach (var coinEntity in coinLoader.Load(moneyCollection.Currency))
            {
                moneyCollection.Coins.Add(coinEntity.Nominal, coinEntity.Count);
            }
        }

        public void AddMoney(MoneyCollection money)
        {
            EnsureMoneyLoaded();
            throw new NotImplementedException();
        }

        public void RemoveMoney(MoneyCollection money)
        {
            EnsureMoneyLoaded();
            throw new NotImplementedException();
        }
    }
}
