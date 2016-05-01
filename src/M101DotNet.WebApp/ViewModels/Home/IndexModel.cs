namespace M101DotNet.WebApp.ViewModels.Home
{
    using System.Collections.Generic;
    using Data.Models;

    using Services.Models;

    public class IndexModel
    {
        public List<PostServiceModel> RecentPosts { get; set; }

        public List<TagServiceModel> Tags { get; set; }
    }
}