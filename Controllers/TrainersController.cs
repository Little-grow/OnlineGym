
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineGym.Models;
using OnlineGym.Repo;
using System.Linq;

namespace OnlineGym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly NotificationService _notificationService;

        public TrainersController(IUnitOfWork unitOfWork, NotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;

        }


        [HttpGet("{coachId}")]
        public async Task<ActionResult<Coach>> DisplayTrainer(int coachId)
        {
            var coach = await _unitOfWork.CoachRepository.Get(coachId);

            if (coach == null)
            {
                return NotFound();
            }

            return coach;
        }


        [HttpPut("{coachId}/edit-profile")]
        public async Task<IActionResult> EditProfile(int coachId, Coach coach)
        {
            await _unitOfWork.CoachRepository.Update(coachId, coach);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("{coachId}/requests/pending")]
        public async Task<ActionResult<IEnumerable<Request>>> GetPendingRequests(int coachId)
        {
            var coach = await _unitOfWork.CoachRepository.Get(coachId);
            var pendingRequests = coach.TrainerRequests.Where(r => r.Status == RequestStatus.Pending);

            if (pendingRequests == null || !pendingRequests.Any())
            {
                return NotFound("No pending coaching requests found.");
            }

            return Ok(pendingRequests);
        }


        [HttpGet("request/Confirm")]
        public async Task<ActionResult<IEnumerable<Request>>> ConfirmRequest(Request request)
        {
            var coach = await _unitOfWork.CoachRepository.Get(request.CoachId);
            await _unitOfWork.RequestRepository.Update(request.Id, request);

            var coachRequest = coach.TrainerRequests.SingleOrDefault(r => r.CoachId == request.CoachId);
            coachRequest.Status = request.Status;

            await _unitOfWork.CoachRepository.Update(request.CoachId, coach);
            await _unitOfWork.SaveChangesAsync();

            _notificationService.SendRequestConfirmedNotifications(request);

            if (request.Status == RequestStatus.Accepted)
            {
                _notificationService.SendCreateTraineeNotifications(request.CoachId, request.TraineeId);
            }

            return Ok("Request confirmed.");
        }


        [HttpPost("create-training-schedule")]
        public async Task<IActionResult> CreateTrainingSchedule(Schedule schedule)
        {
            _unitOfWork.ScheduleRepository.Create(schedule);

            var coachId = schedule.CoachId;
            var coach = await _unitOfWork.CoachRepository.Get(coachId);
            if (coach != null)
            {
                coach.Schedules.Add(schedule);
                await _unitOfWork.CoachRepository.Update(coachId, coach);
            }

            await _unitOfWork.SaveChangesAsync();
            return Ok(schedule);
        }


        [HttpGet("{coachId}/display-notifications")]
        public IActionResult DisplayNotifications(int coachId)
        {
            var notifications = _unitOfWork.NotificationRepository.GetAll()?.Where(n => n.UserId == coachId);
            return Ok(notifications);
        }


        [HttpPut("{coachId}/deactivate-account")]
        public async Task<IActionResult> DeActivateAccount(int coachId)
        {
            var coach = await _unitOfWork.CoachRepository.Get(coachId);
            coach.IsActive = false;
            await _unitOfWork.CoachRepository.Update(coachId, coach);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Account deactivated.");
        }


        [HttpGet("{coachId}/trainees")]
        public async Task<ActionResult<IEnumerable<Trainee>>> GetTrainees(int coachId)
        {
            var coach = await _unitOfWork.CoachRepository.Get(coachId);
            var trainees = _unitOfWork.TraineeRepository.GetAll().Where(t => t.Schedule.CoachId == coachId);

            if (trainees == null || !trainees.Any())
            {
                return NotFound("No trainees found.");
            }
            return Ok(trainees);
        }

        [HttpGet("{coachId}/Schedules")]
        public async Task<IActionResult> GetSchedules(int coachId)
        {
            var coach = await _unitOfWork.CoachRepository.Get(coachId);
            var schedules = coach.Schedules;
            return Ok(schedules);
        }
    }
}
