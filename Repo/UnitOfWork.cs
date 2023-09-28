using OnlineGym.Models;

namespace OnlineGym.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IBaseRepository<Admin> _adminRepository;
        private IBaseRepository<Coach> _trainerRepository;
        private IBaseRepository<Trainee> _traineeRepository;
        private IBaseRepository<Schedule> _scheduleRepository;
        private IBaseRepository<Workout> _workoutRepository;
        private IBaseRepository<Notification> _notificationRepository;
        private IBaseRepository<Request> _requestRepository;
        private IBaseRepository<Update> _updateRepository;
        // Add fields for other repositories as needed

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IBaseRepository<Admin> AdminRepository
        {
            get
            {
                return _adminRepository ??= new BaseRepository<Admin>(_context);
            }
        }

        public IBaseRepository<Coach> CoachRepository
        {
            get
            {
                return _trainerRepository ??= new BaseRepository<Coach>(_context);
            }
        }

        public IBaseRepository<Trainee> TraineeRepository
        {
            get
            {
                return _traineeRepository ??= new BaseRepository<Trainee>(_context);
            }
        }

        public IBaseRepository<Schedule> ScheduleRepository
        {
            get
            {
                return _scheduleRepository ??= new BaseRepository<Schedule>(_context);
            }
        }

        public IBaseRepository<Workout> WorkoutRepository
        {
            get
            {
                return _workoutRepository ??= new BaseRepository<Workout>(_context);
            }
        }

        public IBaseRepository<Notification> NotificationRepository
        {
            get
            {
                return _notificationRepository ??= new BaseRepository<Notification>(_context);
            }
        }

        public IBaseRepository<Request> RequestRepository
        {
            get
            {
                return _requestRepository ??= new BaseRepository<Request>(_context);
            }
        }

        public IBaseRepository<Update> UpdateRepository
        {
            get
            {
                return _updateRepository ??= new BaseRepository<Update>(_context);
            }
        }

        // Add properties and fields for other repositories as needed

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
