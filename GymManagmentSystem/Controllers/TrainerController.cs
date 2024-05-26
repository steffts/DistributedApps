using AutoMapper;
using GymManagmentSystem.Dto;
using GymManagmentSystem.Interfaces;
using GymManagmentSystem.Models;
using GymManagmentSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GymManagmentSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IMapper _mapper;

        public TrainerController(ITrainerRepository trainerRepository, IMapper mapper)
        {
            _trainerRepository = trainerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Trainer>))]
        public IActionResult GetTrainers()
        {
            var trainers = _mapper.Map<List<TrainerDto>>(_trainerRepository.GetTrainers());
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(trainers);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Trainer))]
        [ProducesResponseType(400)]
        public IActionResult GetTrainer(int id)
        {
            if (!_trainerRepository.TrainersExists(id))
                return NotFound();

            var trainers = _mapper.Map<TrainerDto>(_trainerRepository.GetTrainer(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(trainers);
        }

        [HttpGet("{id}/fname")]
        [ProducesResponseType(200, Type = typeof(Trainer))]
        [ProducesResponseType(400)]
        public IActionResult GetTrainerByFName(string fname)
        {
            if (!_trainerRepository.TrainersFNameExists(fname))
                return NotFound();

            var fnames = _mapper.Map<TrainerDto>(_trainerRepository.GetTrainerByFName(fname));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(fnames);
        }

        [HttpGet("{id}/lname")]
        [ProducesResponseType(200, Type = typeof(Member))]
        [ProducesResponseType(400)]
        public IActionResult GetTrainerByLName(string lname)
        {
            if (!_trainerRepository.TrainersLNameExists(lname))
                return NotFound();

            var lnames = _mapper.Map<TrainerDto>(_trainerRepository.GetTrainerByLName(lname));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(lnames);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateTrainer([FromBody] TrainerDto trainerDto)
        {
            if (trainerDto == null)
                return BadRequest();

            var trainer = _trainerRepository.GetTrainers()
                .Where(t => t.FirstName == trainerDto.FirstName)
                .FirstOrDefault();

            if (trainer != null)
            {
                ModelState.AddModelError("", "Trainer already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var trainerMap = _mapper.Map<Trainer>(trainerDto);

            if (!_trainerRepository.CreateTrainer(trainerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully create");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTrainer(int id, [FromBody] TrainerDto trainerDto)
        {
            if (trainerDto == null)
                return BadRequest(ModelState);

            if (id != trainerDto.Id)
                return BadRequest(ModelState);

            if (!_trainerRepository.TrainersExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var trainerMap = _mapper.Map<Trainer>(trainerDto);

            if (!_trainerRepository.UpdateTrainer(trainerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating trainer");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTrainer(int id)
        {
            if (!_trainerRepository.TrainersExists(id))
            {
                return NotFound();
            }

            var trainerDel = _trainerRepository.GetTrainer(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_trainerRepository.DeleteTrainer(trainerDel))
            {
                ModelState.AddModelError("", "Something went wrong deleting trainer");
            }

            return NoContent();
        }
    }
}
