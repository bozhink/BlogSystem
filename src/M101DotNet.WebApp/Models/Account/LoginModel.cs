namespace M101DotNet.WebApp.Models.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class LoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }
}