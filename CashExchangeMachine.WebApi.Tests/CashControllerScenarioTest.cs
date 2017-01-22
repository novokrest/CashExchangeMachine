using System;
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

        //[Test]
        //public void Given_EmptyCashState_And_NoteInserted_Then_MakeExchangeRequest_Should_ReturnInsertedMoney()
        //{
        //    SetNoMoney();
        //    InsertNote(1);

        //    var response = MakeExchangeRequest();
        //    Assert.IsFalse(response.IsSuccessStatusCode);

        //    MoneyResult result = Extract<MoneyResult>(response);
        //    Assert.IsEmpty(result.Coins);
        //    Assert.IsNotEmpty(result.Notes);
        //    Assert.IsTrue(result.HasNotes(1, 1));
        //}

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
            return HttpClient.SendAsync(CreateRequest("api/cashmachine/money", HttpMethod.Post, money)).Result;
        }

        private HttpResponseMessage GetAvailableMoney()
        {
            return HttpClient.GetAsync("api/cashmachine/money").Result;
        }

        private T Extract<T>(HttpResponseMessage response)
        {
            throw new NotImplementedException();
        }
    }
}
