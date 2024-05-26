using GymManagmentSystem.Models;

namespace GymManagmentSystem.Interfaces
{
    public interface IGymRepository
    {
        ICollection<Class> GetClasses();

        Class GetClass(int id);
        Class GetClass(string name);
        bool ClassesExists(int id);
        bool ClassesExists(string name);
        ICollection<Trainer> GetClassesByTrainer(int Id);
        bool CreateClasses(Class classes);
        bool Save();
        bool UpdateClass(Class existingClass);
        bool DeleteClass(Class existingClass);
    }
}
