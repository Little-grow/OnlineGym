namespace OnlineGym.Models
{
    public class Coach: User
    {
        public int YearOfExperience { get; set; }
        public string Speciality { get; set; }
        public bool IsActive { get; set; }
        public List<Request> TrainerRequests { get; set; }
        public List<Schedule> Schedules { get; set; }
    }
}
