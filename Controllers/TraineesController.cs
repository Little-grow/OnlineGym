using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineGym.Models;
using OnlineGym.Repo;

namespace OnlineGym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraineesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly NotificationService _notificationService;

        public TraineesController(IUnitOfWork unitOfWork, NotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;

        }


        [HttpGet("{id}")]
        public IActionResult DisplayTrainee(int id)
        {
            var trainee = _unitOfWork.TraineeRepository.Get(id);
            return Ok(trainee);
        }


        [HttpPut("{id}/edit-profile")]
        public async Task<IActionResult> EditProfile(int id, Trainee trainee)
        {
            await _unitOfWork.TraineeRepository.Update(id, trainee);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("display-all-coaches")]
        public IActionResult DisplayAllCoaches()
        {
            var coaches = _unitOfWork.CoachRepository.GetAll();
            var activeCoaches = coaches.Where(c => c.IsActive == true);
            return Ok(activeCoaches);
        }


        [HttpPost("{traineeId}/applyFor/{coachId}")]
        public async Task<IActionResult> ApplyForCoach(Request request)
        {
            var traineeRequests = TraineeRequestsCoach(request.TraineeId);

            if (traineeRequests != null)
            {
                return BadRequest("You already have applied for a coach,  ypu can apply only for one coach");
            }

            request.Status = RequestStatus.Pending;

            var coach = await _unitOfWork.CoachRepository.Get(request.CoachId);
            coach.TrainerRequests.Add(request);


            _unitOfWork.RequestRepository.Create(request);
            await _unitOfWork.CoachRepository.Update(request.CoachId, coach);

            await _unitOfWork.SaveChangesAsync();
            _notificationService.SendRequestNotifications(request.CoachId, request.TraineeId);

            return Ok("Coaching request created.");
        }


        private async Task<List<Request>?> TraineeRequestsCoach(int traineeId)
        {
            var trainee = await _unitOfWork.TraineeRepository.Get(traineeId);

            if (trainee == null)
            {
                return null;
            }

            if (trainee.TraineeRequests != null)
            {
                return trainee.TraineeRequests.ToList();
            }

            return null;
        }


        [HttpPost("trainees/{traineeId}/weight")]
        public async Task<IActionResult> UpdateTraineeWeight(int traineeId, double newWeight)
        {
            // Check if trainee exists and update their weight
            var trainee = await _unitOfWork.TraineeRepository.Get(traineeId);

            if (trainee == null)
            {
                return NotFound("Trainee not found.");
            }

            trainee.Weight = newWeight;
            await _unitOfWork.TraineeRepository.Update(traineeId, trainee);

            //create new update
            var update = new Update
            {
                TraineeId = traineeId,
                CoachId = trainee.Schedule.CoachId,
                weight = newWeight,
                DateTime = DateTime.Now
            };

            _unitOfWork.UpdateRepository.Create(update);

            await _unitOfWork.SaveChangesAsync();

            return Ok($"Trainee's weight has been updated to {newWeight} kg.");
        }


        [HttpGet("{traineeId}/display-notifications")]
        public IActionResult DisplayNotifications(int traineeId)
        {
            var notifications = _unitOfWork.NotificationRepository.GetAll()?.Where(n => n.UserId == traineeId);
            return Ok(notifications);
        }

        [HttpPut("{traineeId}/deactivate-account")]
        public async Task<IActionResult> DeActivateAccount(int traineeId)
        {
            var trainee = await _unitOfWork.TraineeRepository.Get(traineeId);

            if (trainee == null)
            {
                return NotFound("Trainee not found.");
            }

            trainee.IsActive = false;

            await _unitOfWork.TraineeRepository.Update(traineeId, trainee);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{traineeId}/display-fitness-updates")]
        public async Task<IActionResult> DisplayFitnessUpdate(int traineeId)
        {
            var trainee = await _unitOfWork.TraineeRepository.Get(traineeId);
            if (trainee == null)
            {
                return NotFound("Trainee not found.");
            }
            var updates = _unitOfWork.UpdateRepository.GetAll()?.Where(u => u.TraineeId == traineeId);
            return Ok(updates);
        }
    }
}

