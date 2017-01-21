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
            var noteLoader = new NoteRepository(_sqlConnectionProvider);
            foreach (var noteEntity in noteLoader.Load(moneyCollection.Currency))
            {
                moneyCollection.Notes.Add(noteEntity.Nominal, noteEntity.Count);
            }            
        }

        private void LoadCoins(MoneyCollection moneyCollection)
        {
            var coinLoader = new CoinRepository(_sqlConnectionProvider);
            foreach (var coinEntity in coinLoader.Load(moneyCollection.Currency))
            {
                moneyCollection.Coins.Add(coinEntity.Nominal, coinEntity.Count);
            }
        }

        public void AddMoney(MoneyCollection money)
        {
            EnsureMoneyLoaded();
            AddNotes(money.Notes, money.Currency);
            AddCoins(money.Coins, money.Currency);
        }

        private void AddNotes(Notes notes, Currency currency)
        {
            // TODO: create repository only one time, cash and then reuse
            var noteRepository = new NoteRepository(_sqlConnectionProvider);

            // TODO: create more friendly API for Notes class
            foreach (var noteNominalCountPair in notes)
            {
                var noteEntity = new NoteEntity
                {
                    Nominal = noteNominalCountPair.Key,
                    Currency = currency.Name,
                    Count = noteNominalCountPair.Value
                };

                noteRepository.Insert(noteEntity);
            }
        }

        private void AddCoins(Coins coins, Currency currency)
        {
            // TODO: create repository only one time, cash and then reuse
            var coinRepository = new CoinRepository(_sqlConnectionProvider);

            // TODO: create more friendly API for Coins class
            foreach (var coinNominalCountPair in coins)
            {
                var coinEntity = new CoinEntity
                {
                    Nominal = coinNominalCountPair.Key,
                    Currency = currency.Name,
                    Count = coinNominalCountPair.Value
                };

                coinRepository.Insert(coinEntity);
            }
        }

        public void RemoveMoney(MoneyCollection money)
        {
            EnsureMoneyLoaded();
            throw new NotImplementedException();
        }
    }
}
