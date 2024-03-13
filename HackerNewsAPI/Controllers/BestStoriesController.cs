using HackerNewsAPI.Interface;
using HackerNewsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BestStoriesController : ControllerBase
    {
        private readonly ILogger<BestStoriesController> _logger;
        private readonly IService _service;

        public BestStoriesController(ILogger<BestStoriesController> logger, IService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("{n}")]      
        public async Task<IEnumerable<Story>> GetStoriesAsync(int n)
        {
            return await _service.GetBestStories(n);
        }     

    }
}
