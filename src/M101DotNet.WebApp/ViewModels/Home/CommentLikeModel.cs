namespace M101DotNet.WebApp.ViewModels.Home
{
    using System.Web.Mvc;

    public class CommentLikeModel
    {
        [HiddenInput(DisplayValue = false)]
        public string PostId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string CommentId { get; set; }
    }
}