using System.Net.Http;
using System.Runtime.CompilerServices;
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

        public static HttpResponseMessage AssertFailed(this HttpResponseMessage response)
        {
            Assert.IsFalse(response.IsSuccessStatusCode);
            return response;
        }

        public static T ExtractJson<T>(this HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
        }
    }
}
