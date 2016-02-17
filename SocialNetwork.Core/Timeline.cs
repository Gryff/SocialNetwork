using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Core
{
    public class Timeline
    {
        public List<Post> Posts = new List<Post>();
        
        public IEnumerable<string> GetTimeline() => 
            this.Posts.Select(p => p.ToOutputFormat());
    }
}
