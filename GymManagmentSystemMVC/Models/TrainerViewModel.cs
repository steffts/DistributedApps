using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymManagmentSystemMVC.Models
{
    public class TrainerViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("First Name")]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Last Name")]
        public string? LastName { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Email")]
        public string? Email { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Phone")]
        public string? Phone { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Specialization")]
        public string? Specialization { get; set; }

        [Required]
        [DisplayName("Started At")]
        public DateTime StartedAt { get; set; }

        [Required]
        [DisplayName("Updated At")]
        public DateTime UpdatedAt { get; set; }
    }
}
