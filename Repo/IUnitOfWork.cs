using OnlineGym.Models;

namespace OnlineGym.Repo
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Admin> AdminRepository { get; }
        IBaseRepository<Coach> CoachRepository { get; }
        IBaseRepository<Trainee> TraineeRepository { get; }
        IBaseRepository<Schedule> ScheduleRepository { get; }
        IBaseRepository<Workout> WorkoutRepository { get; }
        IBaseRepository<Notification> NotificationRepository { get; }
        IBaseRepository<Request> RequestRepository { get; }
        IBaseRepository<Update> UpdateRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
