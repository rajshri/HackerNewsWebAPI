using HackerNewsAPI;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.IntegrationTest
{
    public class TestFactory : WebApplicationFactory<Startup>
    {      
        public TestFactory()
        {
           
        }
    }

}
