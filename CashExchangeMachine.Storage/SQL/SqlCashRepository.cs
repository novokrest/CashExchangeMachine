using CashExchangeMachine.Core.Money;
using System.Collections.Generic;
using CashExchangeMachine.Core;
using CashExchangeMachine.Core.Extensions;

namespace CashExchangeMachine.Storage.Sql
{
    public class SqlCashRepository : ICashRepository
    {
        private readonly IDictionary<Currency, MoneyCollection> _totalMoney = new Dictionary<Currency, MoneyCollection>();
        private readonly ISqlConnectionProvider _sqlConnectionProvider;
        
        public SqlCashRepository(ISqlConnectionProvider sqlConnectionProvider)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
        }

        public MoneyCollection LoadMoney(Currency currency)
        {
            EnsureMoneyLoaded(currency);
            return _totalMoney[currency];
        }

        private void EnsureMoneyLoaded(Currency currency)
        {
            if (!_totalMoney.ContainsKey(currency))
            {
                var money = MoneyCollection.Create(currency);
                LoadNotes(money);
                LoadCoins(money);
                _totalMoney.Add(currency, money);
            }
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
                Verifiers.Verify(coinEntity.Count >= 0, "Coins count is less than zero: {0}", coinEntity.Count);
                moneyCollection.Coins.Add(coinEntity.Nominal, coinEntity.Count);
            }
        }

        public void AddMoney(MoneyCollection money)
        {
            EnsureMoneyLoaded(money.Currency);
            AddNotes(money.Notes, money.Currency);
            AddCoins(money.Coins, money.Currency);
        }

        private void AddNotes(Notes notes, Currency currency)
        {
            // TODO: create repository only one time, cash and then reuse
            var noteRepository = new NoteRepository(_sqlConnectionProvider);

            foreach (var noteEntity in ConvertToNoteEntities(notes, currency))
            {
                noteRepository.Insert(noteEntity);
            }

            _totalMoney[currency].Notes.Add(notes);
        }

        private void AddCoins(Coins coins, Currency currency)
        {
            // TODO: create repository only one time, cash and then reuse
            var coinRepository = new CoinRepository(_sqlConnectionProvider);

            // TODO: create more friendly API for Coins class
            foreach (var coinEntity in ConvertToCoinEntities(coins, currency))
            {
                coinRepository.Insert(coinEntity);
            }

            _totalMoney[currency].Coins.Add(coins);
        }

        public void RemoveMoney(MoneyCollection money)
        {
            EnsureMoneyLoaded(money.Currency);
            RemoveNotes(money.Notes, money.Currency);
            RemoveCoins(money.Coins, money.Currency);
        }

        public void SetMoney(MoneyCollection money)
        {
            RemoveMoney(LoadMoney(money.Currency));
            AddMoney(money);
        }

        private void RemoveNotes(Notes notes, Currency currency)
        {
            // TODO: !!!!
            var noteRepository = new NoteRepository(_sqlConnectionProvider);

            foreach (var noteEntity in ConvertToNoteEntities(notes, currency))
            {
                noteRepository.Delete(noteEntity);
            }

            _totalMoney[currency].Notes.Remove(notes);
        }

        private void RemoveCoins(Coins coins, Currency currency)
        {
            var coinRepository = new CoinRepository(_sqlConnectionProvider);

            foreach (var coinEntity in ConvertToCoinEntities(coins, currency))
            {
                coinRepository.Delete(coinEntity);
            }

            _totalMoney[currency].Coins.Remove(coins);
        }

        private IEnumerable<NoteEntity> ConvertToNoteEntities(Notes notes, Currency currency)
        {
            // TODO: create more friendly API for Notes class
            foreach (var noteNominalCountPair in notes)
            {
                var noteEntity = new NoteEntity
                {
                    Nominal = noteNominalCountPair.Key,
                    Currency = currency.Name,
                    Count = noteNominalCountPair.Value
                };

                yield return noteEntity;
            }
        }

        private IEnumerable<CoinEntity> ConvertToCoinEntities(Coins coins, Currency currency)
        {
            // TODO: create more friendly API for Notes class
            foreach (var coinNominalCountPair in coins)
            {
                var coinEntity = new CoinEntity
                {
                    Nominal = coinNominalCountPair.Key,
                    Currency = currency.Name,
                    Count = coinNominalCountPair.Value
                };

                yield return coinEntity;
            }
        }
    }
}
