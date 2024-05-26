namespace GymManagmentSystem.Interfaces
{
    public interface IMemberRepository
    {
        ICollection<Member> GetMembers();

        Member GetMember(int id);
        Member GetMemberByFName(string fname);
        Member GetMemberByLName(string lname);
        bool MembersExists(int id);
        bool MembersFNameExists(string fname);
        bool MembersLNameExists(string lname);
        bool CreateMember(Member member);
        bool Save();
        bool UpdateMember(Member existingMember);
        bool DeleteMember(Member existingMember);
    }
}
