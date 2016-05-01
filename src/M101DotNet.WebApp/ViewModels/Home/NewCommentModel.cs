namespace M101DotNet.WebApp.ViewModels.Home
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class NewCommentModel
    {
        [HiddenInput(DisplayValue = false)]
        public string PostId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Content { get; set; }
    }
}