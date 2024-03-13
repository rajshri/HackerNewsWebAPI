using HackerNewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNewsAPI.Interface
{
    public interface IService
    {
        Task<IEnumerable<Story>> GetBestStories(int count);       
    }
}
