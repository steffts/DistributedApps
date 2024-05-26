using AutoMapper;
using GymManagmentSystem.Dto;
using GymManagmentSystem.Models;

namespace GymManagmentSystem.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Class, ClassDto>().ReverseMap();
            CreateMap<Member, MemberDto>().ReverseMap();
            CreateMap<Trainer, TrainerDto>().ReverseMap();
        }
    }
}
