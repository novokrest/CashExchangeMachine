using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;

namespace CashExchangeMachine.WebApi.HttpActionResults
{
    public class Forbidden<T> : IHttpActionResult
    {
        private readonly T _content;

        public Forbidden(T content)
        {
            _content = content;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Forbidden)
            {
                Content = new ObjectContent(typeof(T), _content, new JsonMediaTypeFormatter())
            };
            return Task.FromResult(response);
        }
    }
}