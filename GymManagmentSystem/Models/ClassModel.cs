using GymManagmentSystem.Models;
using System;
using System.ComponentModel.DataAnnotations;
namespace GymManagmentSystem;
public class Class
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string? Name { get; set; }

    [Required]
    [StringLength(50)]
    public string? Description { get; set; }

    [Required]
    public int TrainerId { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [Required]
    public long Capacity {  get; set; }

    [Required]
    public double Price { get; set; }
}
