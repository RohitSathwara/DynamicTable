using DT.Models;
using Microsoft.EntityFrameworkCore;

namespace DT.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TimeTable> TimeTables { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }
}
