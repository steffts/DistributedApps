using AutoMapper;
using GymManagmentSystem.Dto;
using GymManagmentSystem.Interfaces;
using GymManagmentSystem.Models;
using GymManagmentSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public MemberController(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Member>))]
        public IActionResult GetMembers()
        {
            var members = _mapper.Map<List<MemberDto>>(_memberRepository.GetMembers());

            return Ok(members);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Member))]
        [ProducesResponseType(400)]
        public IActionResult GetMember(int id)
        {
            if (!_memberRepository.MembersExists(id))
                return NotFound();

            var members = _mapper.Map<MemberDto>(_memberRepository.GetMember(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(members);
        }

        [HttpGet("{id}/fname")]
        [ProducesResponseType(200, Type = typeof(Member))]
        [ProducesResponseType(400)]
        public IActionResult GetMemberByFName(string fname)
        {
            if (!_memberRepository.MembersFNameExists(fname))
                return NotFound();

            var fnames = _memberRepository.GetMemberByFName(fname);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(fnames);
        }

        [HttpGet("{id}/lname")]
        [ProducesResponseType(200, Type = typeof(Member))]
        [ProducesResponseType(400)]
        public IActionResult GetMemberByLName(string lname)
        {
            if (!_memberRepository.MembersLNameExists(lname))
                return NotFound();

            var lnames = _memberRepository.GetMemberByLName(lname);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(lnames);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMember([FromBody] MemberDto memberDto)
        {
            if (memberDto == null)
                return BadRequest(ModelState);

            var member = _memberRepository.GetMembers()
                .Where(m => m.FirstName == memberDto.FirstName)
                .FirstOrDefault();

            if (member != null)
            {
                ModelState.AddModelError("", "Member already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var memberMap = _mapper.Map<Member>(memberDto);

            if (!_memberRepository.CreateMember(memberMap))
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
        public IActionResult UpdateMember(int id, [FromBody] MemberDto memberDto)
        {
            if (memberDto == null)
                return BadRequest(ModelState);

            if (id != memberDto.Id)
                return BadRequest(ModelState);

            if (!_memberRepository.MembersExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var memberMap = _mapper.Map<Member>(memberDto);

            if (!_memberRepository.UpdateMember(memberMap))
            {
                ModelState.AddModelError("", "Something went wrong updating member");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteMember(int id)
        {
            if (!_memberRepository.MembersExists(id))
            {
                return NotFound();
            }

            var memberDel = _memberRepository.GetMember(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_memberRepository.DeleteMember(memberDel))
            {
                ModelState.AddModelError("", "Something went wrong deleting member");
            }

            return NoContent();
        }
    }
}
