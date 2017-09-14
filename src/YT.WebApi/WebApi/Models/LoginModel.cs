using System.ComponentModel.DataAnnotations;

namespace YT.WebApi.Models
{
    public class LoginModel
    {

        [Required]
        public string UsernameOrEmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
