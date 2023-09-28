using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OnlineGym.Authentication.Models
{
    public class TokenRequestModel
    {
        [Required]
        public string UserName { get; set; }
        [Required, PasswordPropertyText]
        public string Password { get; set; }
    }
}


