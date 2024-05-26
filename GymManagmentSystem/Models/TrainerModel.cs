namespace GymManagmentSystem.Models;
using System;
using System.ComponentModel.DataAnnotations;

public class Trainer
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
    public string? Phone { get; set; }

    [Required]
    [StringLength(50)]
    public string? Specialization { get; set; }

    [Required]
    public DateTime StartedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }
}
