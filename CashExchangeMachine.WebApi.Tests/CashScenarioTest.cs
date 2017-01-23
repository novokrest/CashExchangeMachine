using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using CashExchangeMachine.Core.Extensions;
using CashExchangeMachine.Core.Money;
using CashExchangeMachine.WebApi.Models;
using CashExchangeMachine.WebApi.Tests.Extensions;
using NUnit.Framework;

namespace CashExchangeMachine.WebApi.Tests
{
    // TODO: Expect that tables are initialized
    [TestFixture]
    internal class CashScenarioTest : ScenarioBaseTest
    {
        [Test]
        public void Given_SetNoMoney_Then_GetAvailableMoney_Should_ReturnNoMoney_With_SuccessStatus()
        {
            SetNoMoney().AssertSuccess();

            GetAvailableMoney().AssertSuccess()
                               .ExtractJson<MoneyInfo>()
                               .AssertNoCoins()
                               .AssertNoNotes();
        }

        [Test]
        public void Given_NoMoneyInserted_Then_MakeExchangeRequest_Should_ReturnErrorStatusCode()
        {
            SetNoMoney().AssertSuccess();

            Exchange().AssertFailed(HttpStatusCode.BadRequest);
        }

        [Test]
        public void Given_NoMoneyState_Then_SetSomeMoney_Then_GetAvailableMoney_Should_ReturnAddedMoney()
        {
            SetNoMoney().AssertSuccess();
            SetMoney(new MoneyBuilder(Currency.Dollar).AddCoins(1, 10).AddNotes(5, 50).Build()).AssertSuccess();
            GetAvailableMoney().AssertSuccess()
                               .ExtractJson<MoneyInfo>()
                               .AssertHasCoins(1, 10)
                               .AssertHasNotes(5, 50);
        }

        [Test]
        public void Given_EmptyCashState_And_NoteInserted_Then_MakeExchangeRequest_Should_ReturnInsertedMoney()
        {
            SetNoMoney().AssertSuccess();
            InsertNote(1).AssertSuccess();
            InsertNote(2).AssertSuccess();
            InsertNote(1).AssertSuccess();

            Exchange().AssertFailed(HttpStatusCode.Forbidden)
                      .ExtractJson<MoneyInfo>()
                      .AssertNoCoins()
                      .AssertHasNotes(1, 2)
                      .AssertHasNotes(2, 1)
                      .AssertHasNotes(5, 0)
                      .AssertHasNotes(10, 0);
        }

        [Test]
        public void Given_EmptyCashState_And_CoinInserted_Then_MakeExchangeRequest_Should_ResturnInsertedMoney()
        {
            SetNoMoney().AssertSuccess();
            InsertCoin(1).AssertSuccess();
            InsertCoin(10).AssertSuccess();
            InsertCoin(10).AssertSuccess();

            Exchange().AssertFailed(HttpStatusCode.Forbidden)
                      .ExtractJson<MoneyInfo>()
                      .AssertNoNotes()
                      .AssertHasCoins(1, 1)
                      .AssertHasCoins(5, 0)
                      .AssertHasCoins(10, 2)
                      .AssertHasCoins(25, 0);
        }

        [Test]
        public void Given_CoinInserted_Then_MakeInsertNoteRequest_Should_ReturnErrorStatusCode()
        {
            SetNoMoney().AssertSuccess();
            InsertCoin(1).AssertSuccess();
            InsertNote(1).AssertFailed(HttpStatusCode.BadRequest);
        }

        [Test]
        public void Given_NoteInserted_Then_MakeInsertCoinRequest_Should_ReturnError()
        {
            SetNoMoney().AssertSuccess();
            InsertNote(5).AssertSuccess();
            InsertCoin(25).AssertFailed(HttpStatusCode.BadRequest);
        }

        [Test]
        public void Given_EnoughMoneyAvailable_And_CoinsInserted_Then_MakeExchangeRequest_Should_ReturnNotesSuccessfully_And_UpdateAvailableMoney()
        {
            var initialMoney = new MoneyBuilder(Currency.Dollar)
                .AddNotes(10, 40)
                .AddNotes(5, 10)
                .AddNotes(2, 20)
                .AddNotes(1, 11)
                .Build();
            var insertedMoney = new MoneyBuilder(Currency.Dollar)
                .AddCoins(25, 1000)
                .AddCoins(10, 1500)
                .AddCoins(5, 1000)
                .AddCoins(1, 5000)
                .Build();
            var exchangeResult = new MoneyBuilder(Currency.Dollar)
                .AddNotes(10, 40)
                .AddNotes(5, 10)
                .AddNotes(2, 20)
                .AddNotes(1, 10)
                .Build();
            var moneyAfterExchange = new MoneyBuilder(Currency.Dollar)
                .AddNotes(1)
                .Build();

            Given_InitialMoney_And_MoneyInserted_Then_MakeExchangeRequest_Should_ReturnExpectedMoney(initialMoney, 
                                                                                                     insertedMoney, 
                                                                                                     HttpStatusCode.OK,
                                                                                                     exchangeResult, 
                                                                                                     moneyAfterExchange);
        }

        [Test]
        public void Given_NotEnoughMoneyAvailable_And_NotesInserted_Then_MakeExchangeRequest_ShouldReturnInsertedNotes_With_ErrorStatus()
        {
            var initialMoney = new MoneyBuilder(Currency.Dollar)
                .AddNotes(1, 2)
                .AddNotes(2, 2)
                .AddNotes(5, 2)
                .AddNotes(10, 2)
                .AddCoins(1, 2)
                .AddCoins(5, 2)
                .AddCoins(10, 2)
                .AddCoins(25, 2)
                .Build();
            var insertedMoney = new MoneyBuilder(Currency.Dollar)
                .AddNotes(1, 10)
                .AddNotes(2, 10)
                .AddNotes(5, 10)
                .AddNotes(10, 10)
                .Build();
            var exchangeResult = insertedMoney;
            var moneyAfterExchange = initialMoney;

            Given_InitialMoney_And_MoneyInserted_Then_MakeExchangeRequest_Should_ReturnExpectedMoney(initialMoney, 
                                                                                                     insertedMoney, 
                                                                                                     HttpStatusCode.Forbidden,
                                                                                                     exchangeResult, 
                                                                                                     moneyAfterExchange);
        }

        [Test]
        public void Given_EnoughMoneyAvailable_And_NotesInserted_Then_MakeExchangeRequest_Should_ReturnCoinsSuccessfully_And_UpdateAvailableMoney()
        {
            var initialMoney = new MoneyBuilder(Currency.Dollar)
                .AddCoins(1, 10000)
                .AddCoins(5, 1000)
                .AddCoins(10, 1500)
                .AddCoins(25, 1000)
                .Build();
            var insertedMoney = new MoneyBuilder(Currency.Dollar)
                .AddNotes(10, 40)
                .AddNotes(5, 10)
                .AddNotes(2, 20)
                .AddNotes(1, 10)
                .Build();
            var exchangeResult = new MoneyBuilder(Currency.Dollar)
                .AddCoins(25, 1000)
                .AddCoins(10, 1500)
                .AddCoins(5, 1000)
                .AddCoins(1, 5000)
                .Build();
            var moneyAfterExchange = new MoneyBuilder(Currency.Dollar)
                .AddCoins(1, 5000)
                .Build();

            Given_InitialMoney_And_MoneyInserted_Then_MakeExchangeRequest_Should_ReturnExpectedMoney(initialMoney,
                                                                                                     insertedMoney,
                                                                                                     HttpStatusCode.OK,
                                                                                                     exchangeResult,
                                                                                                     moneyAfterExchange);
        }

        [Test]
        public void Given_NotEnoughMoneyAvailable_And_CoinsInserted_Then_MakeExchangeRequest_ShouldReturnInsertedCoins_With_ErrorStatus()
        {
            var initialMoney = new MoneyBuilder(Currency.Dollar)
                .AddNotes(1, 2)
                .AddNotes(2, 2)
                .AddNotes(5, 2)
                .AddNotes(10, 2)
                .AddCoins(1, 2)
                .AddCoins(5, 2)
                .AddCoins(10, 2)
                .AddCoins(25, 2)
                .Build();
            var insertedMoney = new MoneyBuilder(Currency.Dollar)
                .AddCoins(1, 100)
                .AddCoins(5, 100)
                .AddCoins(10, 100)
                .AddCoins(25, 1000)
                .Build();
            var exchangeResult = insertedMoney;
            var moneyAfterExchange = initialMoney;

            Given_InitialMoney_And_MoneyInserted_Then_MakeExchangeRequest_Should_ReturnExpectedMoney(initialMoney,
                                                                                                     insertedMoney,
                                                                                                     HttpStatusCode.Forbidden,
                                                                                                     exchangeResult,
                                                                                                     moneyAfterExchange);
        }

        private void Given_InitialMoney_And_MoneyInserted_Then_MakeExchangeRequest_Should_ReturnExpectedMoney(MoneyCollection initialMoney, 
                                                                                                              MoneyCollection insertedMoney,
                                                                                                              HttpStatusCode expectedStatusCode,
                                                                                                              MoneyCollection expectedExchangeResult, 
                                                                                                              MoneyCollection expectedMoneyAfterExchange)
        {
            SetMoney(initialMoney).AssertSuccess();

            insertedMoney.Notes.SelectMany(noteNominalCounPair => 
                                           Enumerable.Repeat(noteNominalCounPair.Key, noteNominalCounPair.Value))
                               .ForEach(noteNominal => InsertNote(noteNominal).AssertSuccess());
            insertedMoney.Coins.SelectMany(coinNominalCounPair =>
                                           Enumerable.Repeat(coinNominalCounPair.Key, coinNominalCounPair.Value))
                               .ForEach(coinNominal => InsertCoin(coinNominal).AssertSuccess());

            var actualExchangeResult = Exchange().AssertStatusCode(expectedStatusCode)
                                                 .ExtractJson<MoneyInfo>()
                                                 .ToMoneyCollection();
            Assert.IsTrue(MoneyCollectionEqualityComparer.IsEquals(expectedExchangeResult, actualExchangeResult));

            var actualMoneyAfterExchange = GetAvailableMoney().AssertSuccess()
                                                              .ExtractJson<MoneyInfo>()
                                                              .ToMoneyCollection();
            Assert.IsTrue(MoneyCollectionEqualityComparer.IsEquals(expectedMoneyAfterExchange, actualMoneyAfterExchange));
        }

        private HttpResponseMessage Exchange()
        {
            return HttpClient.SendAsync(CreateRequest("api/cashmachine/exchange", HttpMethod.Post)).Result;
        }

        private HttpResponseMessage SetNoMoney()
        {
            var money = new MoneyBuilder(Currency.Dollar).Build();
            return SetMoney(money);
        }

        private HttpResponseMessage SetMoney(MoneyCollection money)
        {
            return HttpClient.SendAsync(CreateRequest("api/cashmachine/money", HttpMethod.Post, MoneyInfo.CreateFrom(money))).Result;
        }

        private HttpResponseMessage GetAvailableMoney()
        {
            return HttpClient.GetAsync("api/cashmachine/money").Result;
        }

        private HttpResponseMessage InsertNote(int nominal)
        {
            return HttpClient.SendAsync(CreateRequest($@"api/cashmachine/insert/note/{nominal}", HttpMethod.Put)).Result;
        }

        private HttpResponseMessage InsertCoin(int nominal)
        {
            return HttpClient.SendAsync(CreateRequest($@"api/cashmachine/insert/coin/{nominal}", HttpMethod.Put)).Result;
        }

        private T Extract<T>(HttpResponseMessage response)
        {
            throw new NotImplementedException();
        }
    }
}
