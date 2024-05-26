using Microsoft.EntityFrameworkCore;
using GymManagmentSystem.Models;
using Microsoft.AspNetCore.Identity.Data;


namespace GymManagmentSystem.DataContext
{
    

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>options) : base(options) { }
        public DbSet<Member> Members { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Trainer> Trainers { get; set; }

    }

}
