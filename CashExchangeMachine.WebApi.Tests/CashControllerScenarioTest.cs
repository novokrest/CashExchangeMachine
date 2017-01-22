﻿using System;
using System.Net;
using System.Net.Http;
using CashExchangeMachine.Core.Money;
using CashExchangeMachine.WebApi.Models;
using CashExchangeMachine.WebApi.Tests.Extensions;
using NUnit.Framework;

namespace CashExchangeMachine.WebApi.Tests
{
    [TestFixture]
    internal class CashControllerScenarioTest : ControllerBaseTest
    {
        // TODO: Now works only if in table exists rows
        [Test]
        public void Given_SetNoMoney_Then_GetAvailableMoney_Should_ReturnNoMoney_With_SuccessStatus()
        {
            SetNoMoney().AssertSuccess();

            GetAvailableMoney().AssertSuccess()
                               .ExtractJson<MoneyResult>()
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
                               .ExtractJson<MoneyResult>()
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
                      .ExtractJson<MoneyResult>()
                      .AssertNoCoins()
                      .AssertHasNotes(1, 2)
                      .AssertHasNotes(2, 1)
                      .AssertHasNotes(5, 0)
                      .AssertHasNotes(10, 0);
        }

        //[Test]
        //public void Given_EmptyCashState_And_CoinInserted_Then_MakeExchangeRequest_Should_ResturnInsertedMoney()
        //{
        //    SetNoMoney();
        //    InsertCoin(1);

        //    var response = MakeExchangeRequest();
        //    Assert.IsFalse(response.IsSuccessStatusCode);

        //    MoneyResult result = Extract<MoneyResult>(response);
        //    Assert.IsEmpty(result.Notes);
        //    Assert.IsNotEmpty(result.Coins);
        //    Assert.IsTrue(result.HasCoins(1, 1));
        //}

        //[Test]
        //public void Given_NoteInserted_Then_MakeInsertCoinRequest_Should_ReturnErrorStatusCode()
        //{
        //    SetNoMoney();
        //    InsertCoin(1);

        //    var response = InsertNote(1);
        //    Assert.IsFalse(response.IsSuccessStatusCode);
        //}

        //[Test]
        //public void Given_CoinInserted_Then_MakeInsertNoteRequest_Should_ReturnError()
        //{
        //    SetNoMoney();
        //    InsertCoin(1);

        //    var response = InsertNote(1);
        //    Assert.IsFalse(response.IsSuccessStatusCode);
        //}

        [Test]
        public void Given_EnoughMoneyAvailable_And_CoinsInserted_Then_MakeExchangeRequest_Should_ReturnNotesSuccessfully_And_UpdateAvailableMoney()
        {
            
        }

        [Test]
        public void Given_NotEnoughMoneyAvailable_And_NotesInserted_Then_MakeExchangeRequest_ShouldReturnInsertedNotes_With_ErrorStatus()
        {
            
        }

        [Test]
        public void Given_EnoughMoneyAvailable_And_NotesInserted_Then_MakeExchangeRequest_Should_ReturnCoinsSuccessfully_And_UpdateAvailableMoney()
        {
            
        }

        [Test]
        public void Given_NotEnoughMoneyAvailable_And_CoinsInserted_Then_MakeExchangeRequest_ShouldReturnInsertedCoins_With_ErrorStatus()
        {

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
            return HttpClient.SendAsync(CreateRequest("api/cashmachine/money", HttpMethod.Post, MoneyResult.CreateFrom(money))).Result;
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
