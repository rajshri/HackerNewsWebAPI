using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNewsAPI.Configurations
{
    public class CacheConfig
    {
        public bool Enabled { get; set; } = true;
        public TimeSpan AbsoluteExpirationRelativeToNow { get; set; }
    }
}
