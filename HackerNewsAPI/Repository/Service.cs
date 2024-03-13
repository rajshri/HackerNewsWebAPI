using HackerNewsAPI.Configurations;
using HackerNewsAPI.Interface;
using HackerNewsAPI.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HackerNewsAPI.Repository;
using HackerNewsAPI.Mapping;

namespace HackerNewsAPI.Repository
{
    public class Service : IService
    {       
        private readonly IOptions<CacheConfig> _cacheConfig;
        private readonly IMemoryCache _cache;       
        private readonly IStoryClient _storyClient;      

        public Service(
            IStoryClient storyClient,
            IOptions<CacheConfig> cacheConfig,        
            IMemoryCache cache)
            {   
                _cache = cache; 
                _storyClient = storyClient;
                _cacheConfig = cacheConfig;
            }

        /// <summary>
        ///     Get  List of best n stories
        /// </summary>      
        /// <returns>
        ///      List of best n stories
        /// </returns>
        public async Task<IEnumerable<Story>> GetBestStories(int count)
        {
            var stories = await _storyClient.GetBestStories();  
            var result = stories.Select(x => x.MapToStory()).OrderByDescending(s => s.Score).Take(count);           

            return result;
        }  
    }
}
