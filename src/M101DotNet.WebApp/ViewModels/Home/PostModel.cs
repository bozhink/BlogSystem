namespace M101DotNet.WebApp.ViewModels.Home
{
    using Services.Models;

    public class PostModel
    {
        public PostServiceModel Post { get; set; }

        public NewCommentModel NewComment { get; set; }
    }
}