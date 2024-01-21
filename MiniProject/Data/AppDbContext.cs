using Microsoft.EntityFrameworkCore;
using MiniProject.Models;

namespace MiniProject.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Intrest> Intrests { get; set; }
        public DbSet<PersonIntrest> PersonIntrests { get; set; }
        public DbSet<Link> Links { get; set; }

        

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        


    }
}
