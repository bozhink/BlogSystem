namespace M101DotNet.WebApp.Models.Home
{
    using System.Web.Mvc;

    public class CommentLikeModel
    {
        [HiddenInput(DisplayValue = false)]
        public string PostId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Index { get; set; }
    }
}