using HackerNewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNewsAPI.Mapping
{
    public static class StoryMapper
    {
        public static Story MapToStory(this StoryResponse data)
        {
            return new Story
            {
                Title = data.Title,
                PostedBy = data.By,
                Uri = data.Url,
                Time = DateTimeOffset.FromUnixTimeSeconds(data.Time).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssK"),
                Score = data.Score,
                CommentCount = data.Descendants
            };
        }

    }
}
