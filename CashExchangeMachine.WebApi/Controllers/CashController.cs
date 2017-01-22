using System;
using System.Web.Http;
using CashExchangeMachine.Core.Machine;
using CashExchangeMachine.WebApi.Models;

namespace CashExchangeMachine.WebApi.Controllers
{
    [RoutePrefix("api/cash")]
    public class CashController : ApiController
    {
        private readonly ICashExchangeMachine _cashExchangeMachine;

        public CashController(ICashExchangeMachine cashExchangeMachine)
        {
            _cashExchangeMachine = cashExchangeMachine;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAvailableCash()
        {
            var availableMoney = MoneyInfo.CreateFrom(_cashExchangeMachine.GetAvailableMoney());
            return Ok(availableMoney);
        }
    }
}