namespace M101DotNet.WebApp.ViewModels.Home
{
    using System.Collections.Generic;
    using Data.Models;

    public class IndexModel
    {
        public List<Post> RecentPosts { get; set; }

        public List<TagProjection> Tags { get; set; }
    }
}