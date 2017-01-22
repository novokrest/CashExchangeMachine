﻿using System;
using System.Web.Http;
using CashExchangeMachine.Core.Machine;
using CashExchangeMachine.WebApi.Models;

namespace CashExchangeMachine.WebApi.Controllers
{
    [RoutePrefix("api/cashmachine")]
    public class CashController : ApiController
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
            var availableMoney = MoneyInfo.CreateFrom(_cashExchangeMachine.GetAvailableMoney());
            return Ok(availableMoney);
        }

        [HttpPost]
        [Route("money")]
        public IHttpActionResult SetAvailableCash()
        {
            return Ok("Inside SetAvailableCash");
        }

        [HttpPut]
        [Route("insert/coin/{nominal:int}")]
        public IHttpActionResult InsertCoin(int nominal)
        {
            return Ok($"Coin was inserted: {nominal}");
        }

        [HttpPut]
        [Route("insert/note/{nominal:int}")]
        public IHttpActionResult InsertNote(int nominal)
        {
            return Ok($"Note was inserted: {nominal}");
        }

        [HttpPost]
        [Route("exchange")]
        public IHttpActionResult Exchange()
        {
            return Ok("Inside Exchange");
        }
    }
}