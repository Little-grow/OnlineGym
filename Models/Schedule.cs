using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineGym.Models
{
    public class Schedule
    {
        public int Id { set; get; }
       
        public int CoachId { get; set; }
        [ForeignKey("Id")]
        public Coach coach { get; set; }

        public int TraineeId { get; set; }
        [ForeignKey("Id")]
        public Trainee trainee { get; set; }

        public int WeekNumbers { get; set; }

        public List<Workout> Workouts { get; set; }

        public bool ResetDay { get; set; }

        [Timestamp]
        public DateTime DateTime { get; set; }

    }
}
