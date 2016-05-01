namespace M101DotNet.WebApp.Models.Home
{
    using System.Collections.Generic;

    public class IndexModel
    {
        public List<Post> RecentPosts { get; set; }

        public List<TagProjection> Tags { get; set; }
    }
}