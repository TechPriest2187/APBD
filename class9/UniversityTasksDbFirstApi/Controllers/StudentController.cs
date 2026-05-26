using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityTasksDbFirstApi.Data;
using UniversityTasksDbFirstApi.DTOs;

namespace UniversityTasksDbFirstApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly UniversityTasksDbContext _context;

        public StudentsController(UniversityTasksDbContext context)
        {
            _context = context;
        }

        [HttpGet("{idStudent}/dashboard")]
        public async Task<IActionResult> GetStudentDashboard(int idStudent)
        {
            // By projecting directly into the DTO using .Select(), EF Core automatically generates 
            // a highly optimized SQL query with JOINs, completely avoiding the N+1 problem.
            var dashboard = await _context.Students
                .AsNoTracking()
                .Where(s => s.StudentId == idStudent)
                .Select(s => new StudentDashboardDto
                {
                    Id = s.StudentId,
                    IndexNumber = s.IndexNumber,
                    FullName = s.FirstName + " " + s.LastName, // Using raw properties as partial properties aren't always translatable to SQL
                    ActiveStatus = s.IsActive,
                    Enrollments = s.Enrollments.Select(e => new StudentEnrollmentDto
                    {
                        CourseId = e.Course.CourseId,
                        CourseCode = e.Course.Code,
                        CourseName = e.Course.Name,
                        Status = e.Status
                    }).ToList(),
                    Submissions = s.Submissions.Select(sub => new SubmissionDto
                    {
                        Id = sub.SubmissionId,
                        StudentId = sub.Student.StudentId,
                        StudentFullName = sub.Student.FirstName + " " + sub.Student.LastName,
                        AssignmentId = sub.Assignment.AssignmentId,
                        AssignmentTitle = sub.Assignment.Title,
                        RepositoryUrl = sub.RepositoryUrl,
                        Status = sub.Status,
                        Score = sub.Score,
                        Feedback = sub.Feedback
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (dashboard == null) return NotFound($"Student with ID {idStudent} not found.");

            return Ok(dashboard);
        }
    }
}