namespace OnlineGym.Repo
{
    public class NotificationJob
    {
        private readonly NotificationService _notificationService;
        private readonly IUnitOfWork _unitOfWork;
   
        public NotificationJob(NotificationService notificationService, IUnitOfWork unitOfWork)
        {

            _notificationService = notificationService;
            _unitOfWork = unitOfWork;
        }

        public void SendWeeklyRequestWeightUpdateNotifications()
        {
            var trainees = _unitOfWork.TraineeRepository.GetAll();
            foreach (var trainee in trainees)
            {
                var coachId = trainee.Schedule.CoachId;
                _notificationService.SendRequestWeightUpdateNotifications(trainee.Id);
            }
        }

        public void SendWeeklyWeightUpdateNotifications()
        {
            var trainees = _unitOfWork.TraineeRepository.GetAll();
            foreach (var trainee in trainees)
            {
                var coachId = trainee.Schedule.CoachId;
                _notificationService.SendWeightUpdateNotifications(coachId, trainee.Id);
            }
        }
    }
}
