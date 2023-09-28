using OnlineGym.Models;

namespace OnlineGym.Repo
{
    public class NotificationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async void SendRequestWeightUpdateNotifications(int traineeId)
        {
            var notification = new Notification
            {
                UserId = traineeId,
                Message = $"Please update your weight to notify your coach"
            };

            _unitOfWork.NotificationRepository.Create(notification);
            await _unitOfWork.SaveChangesAsync();
        }

        public async void SendWeightUpdateNotifications(int coachId, int traineeId)
        {
            var trainer = await _unitOfWork.CoachRepository.Get(coachId);
            var trainee = await _unitOfWork.TraineeRepository.Get(traineeId);

            // Create a new notification
            var notification = new Notification
            {
                UserId = trainer.Id, // Set the trainer's ID as the recipient
                Message = $"Trainee with ID {traineeId} has updated their weight to {trainee.Weight} kg."
            };

            _unitOfWork.NotificationRepository.Create(notification);
            await _unitOfWork.SaveChangesAsync();
        }

        public async void SendRequestNotifications(int coachId, int traineeId)
        {
            var trainer = await _unitOfWork.CoachRepository.Get(coachId);
            var trainee = await _unitOfWork.TraineeRepository.Get(traineeId);
            // Create a new notification
            var notification = new Notification
            {
                UserId = trainer.Id, // Set the trainer's ID as the recipient
                Message = $"Trainee with ID {traineeId} has sent you a coaching request."
            };
            _unitOfWork.NotificationRepository.Create(notification);
            await _unitOfWork.SaveChangesAsync();
        }

        public async void SendRequestConfirmedNotifications(Models.Request request)
        {
            var trainer = await _unitOfWork.CoachRepository.Get(request.CoachId);
            var trainee = await _unitOfWork.TraineeRepository.Get(request.TraineeId);

            // Create a new notification
            var notification = new Notification
            {
                UserId = trainee.Id, // Set the trainee's ID as the recipient
                Message = $"Your coaching request has been {request.Status} by trainer with ID {trainer.Id}."
            };

            _unitOfWork.NotificationRepository.Create(notification);
            await _unitOfWork.SaveChangesAsync();
        }

        public async void SendCreateTraineeNotifications(int coachId, int traineeId)
        {
            var trainer = await _unitOfWork.CoachRepository.Get(coachId);
            var trainee = await _unitOfWork.TraineeRepository.Get(traineeId);

            var notification = new Notification
            {
                UserId = trainer.Id,
                Message = $"You should create trainee schedule for trainee {traineeId}"
            };

            _unitOfWork.NotificationRepository.Create(notification);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
