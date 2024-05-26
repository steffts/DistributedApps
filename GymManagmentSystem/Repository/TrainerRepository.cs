using AutoMapper;
using GymManagmentSystem.Interfaces;
using GymManagmentSystem.Models;

namespace GymManagmentSystem.Repository
{
    public class TrainerRepository : ITrainerRepository
    {
        private readonly DataContext.DataContext _context;
        private readonly IMapper _mapper;
        public TrainerRepository(DataContext.DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public Trainer GetTrainer(int id)
        {
            return _context.Trainers.Where(t => t.Id == id).FirstOrDefault();
        }

        public Trainer GetTrainerByFName(string fname)
        {
            return _context.Trainers.Where(t => t.FirstName == fname).FirstOrDefault();
        }

        public Trainer GetTrainerByLName(string lname)
        {
            return _context.Trainers.Where(t => t.LastName == lname).FirstOrDefault();
        }

        public bool TrainersExists(int id)
        {
            return _context.Trainers.Any(t => t.Id == id);
        }

        bool ITrainerRepository.TrainersFNameExists(string fname)
        {
            return _context.Trainers.Any(t => t.FirstName == fname);
        }

        bool ITrainerRepository.TrainersLNameExists(string lname)
        {
            return _context.Trainers.Any(t => t.LastName == lname);
        }
        public bool CreateTrainer(Trainer trainers)
        {
            _context.Add(trainers);
            return Save();
        }

        public bool UpdateTrainer(Trainer existingTrainer)
        {
            _context.Trainers.Update(existingTrainer);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteTrainer(Trainer existingTrainer)
        {
            _context.Trainers.Remove(existingTrainer);
            return _context.SaveChanges() > 0;
        }


        public ICollection<Trainer> GetTrainers()
        {
            return _context.Trainers.OrderBy(t => t.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
