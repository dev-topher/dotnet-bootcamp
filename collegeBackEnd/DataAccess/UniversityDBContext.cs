using collegeBackEnd.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace collegeBackEnd.DataAccess
{
    public class UniversityDBContext: DbContext
    {
        public UniversityDBContext(DbContextOptions<UniversityDBContext> options): base(options) 
        { 
            // Add dbsets o tablas
        }
            
        public DbSet<User>? Users { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Chapter>? Chapters { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Student>? Students { get; set; }
    }
}
