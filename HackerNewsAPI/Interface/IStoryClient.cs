using HackerNewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNewsAPI.Interface
{
    public interface IStoryClient
    {
        public Task<List<StoryResponse>> GetBestStories();

        public Task<long[]> GetBestStoriesIDs();

        public Task<StoryResponse> GetStoryById(long id);

        public  Task<StoryResponse> FetchStoryById(long id);

     }
}
