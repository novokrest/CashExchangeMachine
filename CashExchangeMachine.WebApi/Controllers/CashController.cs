using System;
using System.Net;
using System.Web.Http;
using CashExchangeMachine.Core.Machine;
using CashExchangeMachine.Core.Money;
using CashExchangeMachine.WebApi.HttpActionResults;
using CashExchangeMachine.WebApi.Models;

namespace CashExchangeMachine.WebApi.Controllers
{
    [RoutePrefix("api/cashmachine")]
    public class CashController : ApiControllerEx
    {
        private readonly ICashExchangeMachine _cashExchangeMachine;

        public CashController(ICashExchangeMachine cashExchangeMachine)
        {
            _cashExchangeMachine = cashExchangeMachine;
        }

        [HttpGet]
        [Route("money")]
        public IHttpActionResult GetAvailableCash()
        {
            var availableMoney = MoneyResult.CreateFrom(_cashExchangeMachine.GetAvailableMoney());
            return Ok(availableMoney);
        }

        [HttpPost]
        [Route("money")]
        public IHttpActionResult SetAvailableCash([FromBody]MoneyResult moneyResult)
        {
            try
            {
                _cashExchangeMachine.SetMoney(moneyResult.ToMoneyCollection());
                return Ok();
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
            
        }

        [HttpPut]
        [Route("insert/coin/{nominal:int}")]
        public IHttpActionResult InsertCoin(int nominal)
        {
            try
            {
                _cashExchangeMachine.InsertCoin(nominal);
                return Ok($"Coin was inserted: {nominal}");
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        [Route("insert/note/{nominal:int}")]
        public IHttpActionResult InsertNote(int nominal)
        {
            try
            {
                _cashExchangeMachine.InsertNote(nominal);
                return Ok($"Note was inserted: {nominal}");
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Route("exchange")]
        public IHttpActionResult Exchange()
        {
            try
            {
                var exchangeResult = _cashExchangeMachine.ConfirmExchange();
                var moneyResult = MoneyResult.CreateFrom(exchangeResult.Money);
                return exchangeResult.Success ? Ok(moneyResult)
                                              : Forbidden(moneyResult);
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}