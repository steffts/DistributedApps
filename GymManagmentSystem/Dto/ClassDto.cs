using System.ComponentModel.DataAnnotations;

namespace GymManagmentSystem.Dto
{
    public class ClassDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int TrainerId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public long Capacity { get; set; }
        public double Price { get; set; }
    }
}
