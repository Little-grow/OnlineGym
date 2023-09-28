using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineGym.Models
{
    public class Request
    {
        public int Id { get; set; }

        public int TraineeId { get; set; }
        [ForeignKey("Id")]
        public Trainee trainee {  get; set; }

        public int CoachId { get; set; }
        [ForeignKey("Id")]
        public Coach coach { get; set; }

        public RequestStatus Status { get; set; }
      
    }
}
