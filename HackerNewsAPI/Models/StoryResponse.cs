﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNewsAPI.Models
{
    public class StoryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Url { get; set; } 
        public int Score { get; set; }       
        public string By { get; set; } 
        public int Descendants { get; set; }
        public long Time { get; set; }        
    }
}
