using Microsoft.EntityFrameworkCore;

namespace OnlineGym.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Update> Updates { get; set; }

    }
}