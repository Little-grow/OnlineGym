using System.ComponentModel.DataAnnotations;

namespace OnlineGym.Models
{
    public class Admin: User
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
