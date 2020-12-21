using MbDotNet;
using MbDotNet.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;

namespace MockTests
{
    [TestClass]
    public class MockTests
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly MountebankClient _mountebankClient = new MountebankClient();

        [DataTestMethod]
        [DataRow("http://localhost:4545/rates/usd", 200, "{\n    \"Currency\": \"usd\",\n    \"Rate\": 73.6\n}", "usd", 73.6)]
        [DataRow("http://localhost:4545/rates/euro", 200, "{\n    \"Currency\": \"euro\",\n    \"Rate\": 89.86\n}", "euro", 89.86)]
        [DataRow("http://localhost:4545/rates/uah", 200, "{\n    \"Currency\": \"uah\",\n    \"Rate\": 2.62\n}", "uah", 2.62)]
        public void GetStatusCode_And_GetResponseContent_WithUrl_ShouldReturnExpectedResults(string url, int statusCode, string content, string currency, double rate)
        {
            _mountebankClient.DeleteAllImposters();
            var imposter = _mountebankClient.CreateHttpImposter(4545);

            var obj = new TestObject { Currency = currency, Rate = rate };

            imposter.AddStub().ReturnsJson(HttpStatusCode.OK, obj)
                .OnPathAndMethodEqual($"/rates/{obj.Currency}", Method.Get);

            _mountebankClient.Submit(imposter);

            var response = _client.GetAsync(url).Result;
            Assert.AreEqual(statusCode, response.StatusCode.GetHashCode());
            Assert.AreEqual(content, response.Content.ReadAsStringAsync().Result);
        }
    }
}
