using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CashExchangeMachine.WebApi.Tests.Extensions
{
    internal static class HttpResponseMessageExtension
    {
        public static HttpResponseMessage AssertSuccess(this HttpResponseMessage response)
        {
            Assert.IsTrue(response.IsSuccessStatusCode);
            return response;
        }

        public static HttpResponseMessage AssertFailed(this HttpResponseMessage response, HttpStatusCode? expectedStatusCode = null)
        {
            Assert.IsFalse(response.IsSuccessStatusCode);
            if (expectedStatusCode.HasValue)
            {
                Assert.AreEqual(expectedStatusCode, response.StatusCode);
            }
            return response;
        }

        public static HttpResponseMessage AssertStatusCode(this HttpResponseMessage response, HttpStatusCode expectedStatusCode)
        {
            Assert.AreEqual(expectedStatusCode, response.StatusCode);
            return response;
        }

        public static T ExtractJson<T>(this HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
        }
    }
}
