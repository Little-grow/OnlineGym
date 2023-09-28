using System.ComponentModel.DataAnnotations;
using OnlineGym.Models;

namespace OnlineGym.Authentication.Models
{
    public class RegisterModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public Role role { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public int age { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public byte[] Image { get; set; }
    }
}


