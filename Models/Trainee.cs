namespace OnlineGym.Models
{
    public class Trainee: User
    {
        public int Height { get; set; }
        public double Weight { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public List<Request> TraineeRequests { get; set; } // would be better to have one request per trainee, but this is fine for now
        public Schedule Schedule { get; set; }
    }
}
