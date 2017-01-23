using System.Web.Http;
using CashExchangeMachine.WebApi.HttpActionResults;

namespace CashExchangeMachine.WebApi.Controllers
{
    public class ApiControllerEx : ApiController
    {
        public IHttpActionResult Forbidden<T>(T content)
        {
            return new Forbidden<T>(content);
        }
    }
}