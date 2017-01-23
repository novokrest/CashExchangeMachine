using CashExchangeMachine.Core.Money;
using CashExchangeMachine.Storage.Sql;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CashExchangeMachine.Storage.IntegrationTests
{
    [TestFixture]
    internal class NoteRepositoryTests
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
        public void CheckAllNotesLoadedSuccessfullyWithoutCheckingNoteContents()
        {
            var noteRepository = GetNoteRepository();

            var loadedNotes = noteRepository.Load(CurrencyRegistry.GetCurrency(Currency));

            Assert.IsTrue(InitNoteEntities.Length == loadedNotes.Count);
        }

        [Test]
        public void Given_NewNotes_Should_AddNotesSuccessfuly()
        {
            var noteRepository = GetNoteRepository();
            // TODO: may be pass just Currency name ?
            var notesBeforeInsertion = LoadNotes(noteRepository);

            var note = new NoteEntity { Nominal = 1, Currency = Currency, Count = 10 };
            noteRepository.Insert(note);

            var notesAfterInsertion = LoadNotes(noteRepository);

            Assert.IsTrue(notesAfterInsertion[1].Count == notesBeforeInsertion[1].Count + 10);
        }

        [Test]
        public void Given_Notes_Should_RemoveNotesSuccessfuly()
        {
            var noteRepository = GetNoteRepository();
            // TODO: may be pass just Currency name ?
            var notesBeforeInsertion = LoadNotes(noteRepository);

            var note = new NoteEntity { Nominal = 1, Currency = Currency, Count = 10 };
            noteRepository.Delete(note);

            var notesAfterInsertion = LoadNotes(noteRepository);

            Assert.IsTrue(notesAfterInsertion[1].Count == notesBeforeInsertion[1].Count - 10);
        }

        private NoteRepository GetNoteRepository()
        {
            var sqlConnectionProvider = new SqlConnectionProvider(ConnectionString);
            var noteRepository = new NoteRepository(sqlConnectionProvider);

            return noteRepository;
        }

        private IDictionary<int, NoteEntity> LoadNotes(NoteRepository noteRepository)
        {
            return noteRepository.Load(CurrencyRegistry.GetCurrency(Currency))
                                                       .ToDictionary(noteEntity => noteEntity.Nominal, 
                                                                     noteEntity => noteEntity);
        }
    }
}
