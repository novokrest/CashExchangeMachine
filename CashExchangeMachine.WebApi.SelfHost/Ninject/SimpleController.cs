using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CashExchangeMachine.WebApi.SelfHost.Ninject
{
    public class SimpleController : ApiController
    {
        private readonly ISimpleDependency _simpleDependency;

        public SimpleController(ISimpleDependency simpleDependency)
        {
            _simpleDependency = simpleDependency;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetState()
        {
            return Ok<string>("simple-state");
        }
    }
}
