using Microsoft.EntityFrameworkCore;
using SRegister.Models;

namespace SRegister.Models 
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // These tell EF Core which tables to create in the database
        public DbSet<Student> Students { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}