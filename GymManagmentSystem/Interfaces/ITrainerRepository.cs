using GymManagmentSystem.Models;

namespace GymManagmentSystem.Interfaces
{
    public interface ITrainerRepository
    {
        ICollection<Trainer> GetTrainers();

        Trainer GetTrainer(int id);
        Trainer GetTrainerByFName(string fname);
        Trainer GetTrainerByLName(string lname);
        bool TrainersExists(int id);
        bool TrainersFNameExists(string fname);
        bool TrainersLNameExists(string lname);
        bool CreateTrainer(Trainer trainer);
        bool Save();
        bool UpdateTrainer(Trainer existingTrainer);
        bool DeleteTrainer(Trainer existingTrainer);
    }
}
