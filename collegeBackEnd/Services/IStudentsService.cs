using collegeBackEnd.Models.DataModels;

namespace collegeBackEnd.Services
{
    public interface IStudentsService
    {
        IEnumerable<Student> GetStudentsWithCourses();
        IEnumerable<Student> GetStudentsWithNoCourses();
    }
}
