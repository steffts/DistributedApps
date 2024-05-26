using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GymManagmentSystem;

namespace GymManagmentSystemMVC.Models
{
    public class ClassViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Name")]
        public string? Name { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Description")]
        public string? Description { get; set; }

        [Required]
        [DisplayName("Trainer")]
        public int TrainerId { get; set; }

        [Required]
        [DisplayName("Start Time")]
        public DateTime StartTime { get; set; }

        [Required]
        [DisplayName("End Time")]
        public DateTime EndTime { get; set; }

        [Required]
        [DisplayName("Capacity")]
        public long Capacity { get; set; }

        [Required]
        [DisplayName("Price")]
        public double Price { get; set; }


    }
}
