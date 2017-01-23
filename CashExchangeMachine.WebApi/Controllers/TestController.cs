using System.Collections.Generic;
using System.Web.Http;

namespace CashExchangeMachine.WebApi.Controllers
{
    public class TestObject
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return $@"{{ Name = ""{Name}"" }}";
        }
    }

    public class TestController : ApiController
    {
        private static readonly TestObject[] Objects = new[]
        {
            new TestObject { Name = "Object1" },
            new TestObject { Name = "Object2" },
            new TestObject { Name = "Object3" }
        };

        public IEnumerable<TestObject> GetAllObjects()
        {
            return Objects;
        }

        public TestObject GetObject(int id)
        {
            return Objects[id - 1];
        }
    }
}
