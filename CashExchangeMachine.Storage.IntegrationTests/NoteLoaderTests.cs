using CashExchangeMachine.Core.Money;
using CashExchangeMachine.Storage.Sql;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashExchangeMachine.Storage.IntegrationTests
{
    [TestFixture]
    internal class NoteLoaderTests
    {
        private const string ConnectionString = @"Data Source=.\SQL2012EXPRESS;Initial Catalog=TestCashDB; Integrated Security=True";
        private const string NoteTableName = "Notes";
        private const string Currency = "Dollar";

        private static readonly NoteEntity[] InitNoteEntities = new[] 
        {
            new NoteEntity { Nominal = 1, Currency = "Dollar", Count = 123 },
            new NoteEntity { Nominal = 2, Currency = "Dollar", Count = 342 },
            new NoteEntity { Nominal = 5, Currency = "Dollar", Count = 9574 },
            new NoteEntity { Nominal = 10, Currency = "Dollar", Count = 332 }
        };

        [SetUp]
        public void SetUp()
        {
            TestTableInitializer.Create(ConnectionString, NoteTableName)
                                .Initialize(InitNoteEntities);
        }

        [Test]
        public void CheckAllEntitiesLoadedSuccessfullyWithoutCheckingNoteContents()
        {
            var sqlConnectionProvider = new SqlConnectionProvider(ConnectionString);
            var noteLoader = new NoteEntityLoader(sqlConnectionProvider);

            var loadedNotes = noteLoader.Load(CurrencyRegistry.GetCurrency(Currency));

            Assert.IsTrue(InitNoteEntities.Length == loadedNotes.Count);
        }
    }
}
