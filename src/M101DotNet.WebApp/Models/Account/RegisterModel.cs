namespace M101DotNet.WebApp.Models.Account
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