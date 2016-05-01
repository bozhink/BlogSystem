namespace M101DotNet.WebApp.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}