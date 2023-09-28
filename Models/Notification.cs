using System.ComponentModel.DataAnnotations;

namespace OnlineGym.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; } = string.Empty;
        [Timestamp]
        public DateTime DateTime { get; set; }
    }
}
