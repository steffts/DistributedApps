using AutoMapper;
using GymManagmentSystem.Dto;
using GymManagmentSystem.Interfaces;
using GymManagmentSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GymManagmentSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IGymRepository _gymRepository;
        private readonly IMapper _mapper;

        public ClassController(IGymRepository gymRepository, IMapper mapper)
        { 
            _gymRepository = gymRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Class>))]
        public IActionResult GetClasses() 
        { 
            var classes = _mapper.Map<List<ClassDto>>(_gymRepository.GetClasses());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(classes);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200,Type = typeof(Class))]
        [ProducesResponseType(400)]
        public IActionResult GetClass(int id) 
        {
            if (!_gymRepository.ClassesExists(id))
                return NotFound();

            var classes = _mapper.Map<ClassDto>(_gymRepository.GetClass(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(classes);
        }

        [HttpGet("{id}/name")]
        [ProducesResponseType(200, Type = typeof(Class))]
        [ProducesResponseType(400)]
        public IActionResult GetClass(string name)
        {
            if (!_gymRepository.ClassesExists(name))
                return NotFound();

            var names = _mapper.Map<ClassDto>(_gymRepository.GetClass(name));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(names);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateClass([FromBody] ClassDto classDto)
        {
            if (classDto == null)
                return BadRequest(ModelState);

            var newClass = _gymRepository.GetClasses()
                .Where(c => c.Name == classDto.Name)
                .FirstOrDefault();

            if (newClass != null)
            {
                ModelState.AddModelError("", "Class already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var classMap = _mapper.Map<Class>(classDto);

            if(!_gymRepository.CreateClasses(classMap))
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
        public IActionResult UpdateClass(int id, [FromBody] ClassDto classDto)
        {
            if (classDto == null)
                return BadRequest(ModelState);

            if (id != classDto.Id)
                return BadRequest(ModelState);

            if (!_gymRepository.ClassesExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var classMap = _mapper.Map<Class>(classDto);

            if (!_gymRepository.UpdateClass(classMap))
            {
                ModelState.AddModelError("", "Something went wrong updating class");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteClass(int id)
        {
            if (!_gymRepository.ClassesExists(id)) 
            {
                return NotFound();
            }

            var classDel = _gymRepository.GetClass(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_gymRepository.DeleteClass(classDel))
            {
                ModelState.AddModelError("", "Something went wrong deleting class");
            }

            return NoContent();
        }


    }
}
