using System;
using System.ComponentModel.DataAnnotations;

namespace GymManagmentSystem;
public class Member
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string? FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string? LastName { get; set; }

    [Required]
    [StringLength(50)]
    public string? Email { get; set; }

    [Required]
    [StringLength(50)]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [StringLength(50)]
    public string? MembershipType { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }
}
