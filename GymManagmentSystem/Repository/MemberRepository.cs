using AutoMapper;
using GymManagmentSystem.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GymManagmentSystem.Repository
{
    public class MemberRepository: IMemberRepository
    {
        private readonly DataContext.DataContext _context;
        private readonly IMapper _mapper;
        public MemberRepository(DataContext.DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public Member GetMember(int id)
        {
            return _context.Members.Where(m => m.Id == id).FirstOrDefault();
        }

        public Member GetMemberByFName(string fname)
        {
            return _context.Members.Where(m => m.FirstName == fname).FirstOrDefault();
        }

        public Member GetMemberByLName(string lname)
        {
            return _context.Members.Where(m => m.LastName == lname).FirstOrDefault();
        }

        public bool MembersExists(int id)
        {
            return _context.Members.Any(m => m.Id == id);
        }

        bool IMemberRepository.MembersFNameExists(string fname)
        {
            return _context.Members.Any(m => m.FirstName == fname);
        }

        bool IMemberRepository.MembersLNameExists(string lname)
        {
            return _context.Members.Any(m => m.LastName == lname);
        }

        public bool CreateMember(Member member)
        {
            _context.Add(member);
            return Save();
        }

        public bool UpdateMember(Member existingMember)
        {
            _context.Members.Update(existingMember);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteMember(Member existingMember)
        {
            _context.Members.Remove(existingMember);
            return _context.SaveChanges() > 0;
        }

        public ICollection<Member> GetMembers()
        {
            return _context.Members.OrderBy(m => m.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
