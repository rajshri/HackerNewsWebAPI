using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc.Testing;
using HackerNewsAPI;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using System.Text.Json;
using HackerNewsAPI.Models;
using Microsoft.Extensions.Options;
using HackerNewsAPI.Configurations;
using System.Net.Http;

namespace HackerNews.IntegrationTest
{
    [TestClass]
    public class IntegrationTest
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly string BaseAddress = "https://localhost:44320/";

        public IntegrationTest()
        {
            _factory = new TestFactory();
            
        }
        private HttpClient Client()
        {
            var client = _factory.CreateClient();
            client.BaseAddress = new System.Uri(BaseAddress);
            return client;
        }
       
        [TestMethod]
        public async Task FetchStorieIDs()
        {
            var client = Client();

            var response = await client.GetAsync("/api/BestStories/200");
            
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
       
    }
}
