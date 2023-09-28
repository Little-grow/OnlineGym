using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineGym.Models;
using OnlineGym.Repo;

namespace OnlineGym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet("{adminId}")]
        public IActionResult DisplayAdmin(int adminId) 
        {
            var admin = _unitOfWork.AdminRepository.Get(adminId);
            return Ok(admin);
        }


        [HttpPut("{adminId}/edit-profile")]
        public async Task<IActionResult> EditProfile(int adminId, Admin admin)
        {
            await _unitOfWork.AdminRepository.Update(adminId, admin);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
        

        [HttpGet]
        public IActionResult DisplayCoaches()
        {
            var coaches = _unitOfWork.CoachRepository.GetAll();
            return Ok(coaches);
        }


        [HttpGet("display-all-trainees")]
        public IActionResult DisplayAllTrainees()
        {
            var trainees = _unitOfWork.TraineeRepository.GetAll();
            return Ok(trainees);
        }


        [HttpGet("display-each-coach-trainees")]
        public async Task<IActionResult> DisplayEachCoachTrainees()
        {
            var coaches = _unitOfWork.CoachRepository.GetAll();

            Dictionary<Coach, List<Trainee>> coachTrainees = new Dictionary<Coach, List<Trainee>>();

            List<Trainee> coachTraineesList;
            foreach (var coach in coaches)
            {
                coachTraineesList = new List<Trainee>();
                foreach (var request in coach.TrainerRequests)
                {
                    if (request.Status == RequestStatus.Accepted)
                    {
                        var trainee = await _unitOfWork.TraineeRepository.Get(request.TraineeId);
                        coachTraineesList.Add(trainee);
                    }
                }
                coachTrainees.Add(coach, coachTraineesList);
            }

            return Ok(coachTrainees);
        }


        [HttpGet("display-each-coach-schedules")]
        public IActionResult DisplayEachCoachSchedules()
        {
            var coaches = _unitOfWork.CoachRepository.GetAll();

            Dictionary<Coach, List<Schedule>> coachSchedules = new();
          
            foreach (var coach in coaches)
            {
                coachSchedules.Add(coach, coach.Schedules);
            }

            return Ok(coachSchedules);
        }


        [HttpGet("display-all-updates")]
        public IActionResult DisplayAllUpdates()
        {
            var updates = _unitOfWork.UpdateRepository.GetAll();
            var trainees = _unitOfWork.TraineeRepository.GetAll();
            Dictionary<Trainee, List<Update>> traineesUpdates = new Dictionary<Trainee, List<Update>>();

            List<Update> traineeUpdates = new();
            foreach (var trainee in trainees)
            {
                traineeUpdates = updates.Where(u => u.TraineeId == trainee.Id).ToList();
                traineesUpdates.Add(trainee, traineeUpdates);
            }

            return Ok(traineesUpdates);
        }
    }
}
