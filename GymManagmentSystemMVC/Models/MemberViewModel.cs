using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymManagmentSystemMVC.Models
{
    public class MemberViewModel
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
        [DisplayName("Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Membership type")]
        public string? MembershipType { get; set; }

        [Required]
        [DisplayName("Created At")]
        public DateTime CreatedAt { get; set; }

        [Required]
        [DisplayName("Updated At")]
        public DateTime UpdatedAt { get; set; }
    }
}
