using GymManagmentSystemMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GymManagmentSystem;

namespace GymManagmentSystemMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ClassViewModel> classViewModels { get; set; }
        public DbSet<MemberViewModel> memberViewModels { get; set; }
        public DbSet<GymManagmentSystem.Member> Member { get; set; } = default!;
        public DbSet<TrainerViewModel> trainerViewModels { get; set; }
    }
}
