using AutoMapper;
using GymManagmentSystem.DataContext;
using GymManagmentSystem.Interfaces;
using GymManagmentSystem.Models;

namespace GymManagmentSystem.Repository
{
    public class ClassRepository : IGymRepository
    {
        private readonly DataContext.DataContext _context;
        private readonly IMapper _mapper;
        public ClassRepository(DataContext.DataContext context, IMapper mapper) 
        {
            _context=context;
            _mapper = mapper;
        
        }

        public Class GetClass(int id)
        {
            return _context.Classes.Where(c => c.Id == id).FirstOrDefault();
        }

        public Class GetClass(string name)
        {
            return _context.Classes.Where(c => c.Name == name).FirstOrDefault();
        }

        public ICollection<Class> GetClasses()
        {
            return _context.Classes.OrderBy(c => c.Id).ToList();
        }


        bool IGymRepository.ClassesExists(int id)
        {
            return _context.Classes.Any(c => c.Id == id);
        }

        bool IGymRepository.ClassesExists(string name)
        {
            return _context.Classes.Any(c => c.Name == name);
        }

        public bool CreateClasses(Class classes)
        {
            _context.Add(classes);
            return Save();
        }
        public bool UpdateClass(Class existingClass)
        {
            _context.Classes.Update(existingClass);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteClass(Class existingClass)
        {
            _context.Classes.Remove(existingClass);
            return _context.SaveChanges() > 0;
        }

        ICollection<Trainer> IGymRepository.GetClassesByTrainer(int Id)
        {
            return _context.Trainers.Where(c => c.Id == Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
