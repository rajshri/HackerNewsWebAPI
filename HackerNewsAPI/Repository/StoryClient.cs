using HackerNewsAPI.Interface;
using HackerNewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using HackerNewsAPI.Configurations;
using System.Net.Http.Json;
using HackerNewsAPI.Repository;

namespace HackerNewsAPI.Repository
{
    public class StoryClient : IStoryClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<API> _api;
        private readonly IOptions<CacheConfig> _cacheConfig;
        private readonly IMemoryCache _cache;
        public StoryClient(IHttpClientFactory httpClientFactory,
            IOptions<API> api,
            IOptions<CacheConfig> cacheConfig,
            IMemoryCache cache)
        {
            _httpClientFactory = httpClientFactory;
            _cacheConfig = cacheConfig;
            _cache = cache;
            _api = api;
        }

        /// <summary>
        ///     Get all story ids and get story details by IDs 
        /// </summary>      
        /// <returns>
        ///      List of best stories
        /// </returns>
       
        public async Task<List<StoryResponse>> GetBestStories()
        {
            var bestStoryIDs = await GetBestStoriesIDs();

            var tasks = bestStoryIDs.Select(Id => GetStoryById(Id));

            var response = await Task.WhenAll(tasks);

            var stories = response
                   .Where(s => s is not null)
                   .ToList();

            return stories;
        }

        public async Task<long[]> GetBestStoriesIDs()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(Keys.Client);
                var url = _api.Value.BestStoriesUrl;

                var response = await httpClient.GetAsync(url);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return Array.Empty<long>();
                }

                var storyIDs = await response.Content.ReadFromJsonAsync<long[]>();
                return storyIDs ?? Array.Empty<long>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///      //Fetch story details from API or from memory cache
        /// </summary>      
        /// <returns>
        ///     story detail by Id
        /// </returns>
        public async Task<StoryResponse> GetStoryById(long Id)
        {
            try
            {
                if (!_cacheConfig.Value.Enabled)
                {
                    return await FetchStoryById(Id);
                }

                var cacheKey = CreateKey(Id);
                
                //Get/Create Cache entry

                var story = await _cache.GetOrCreateAsync(
                    cacheKey,
                    async cacheEntry =>
                    { 
                        ConfigureCacheExpiration(cacheEntry);
                        return await FetchStoryById(Id);
                    });

                return story;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///      //Fetch best story details from url
        /// </summary>      
        /// <returns>
        ///     story detail by Id
        /// </returns>
        public async Task<StoryResponse> FetchStoryById(long id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient(Keys.Client);
                var url = string.Format(_api.Value.StoryDetailUrl, id);

                var response = await client.GetAsync(url);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                var story = await response.Content.ReadFromJsonAsync<StoryResponse>();
                return story;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ConfigureCacheExpiration(ICacheEntry cacheEntry)
        {
            cacheEntry.AbsoluteExpirationRelativeToNow = _cacheConfig.Value.AbsoluteExpirationRelativeToNow;
        }       

        private static string CreateKey(long id)
        {
            return $"{Keys.StoryID}_{id}";
        }

    }
}

