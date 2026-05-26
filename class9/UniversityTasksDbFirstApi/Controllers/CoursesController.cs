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
    public class CoursesController : ControllerBase
    {
        private readonly UniversityTasksDbContext _context;

        public CoursesController(UniversityTasksDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses([FromQuery] bool activeOnly = true)
        {
            var courses = await _context.Courses
                .AsNoTracking()
                .Select(c => new CourseDto
                {
                    Id = c.CourseId,
                    Code = c.Code,
                    Name = c.Name,
                    Credits = c.Credits,
                    AssignmentCount = c.Assignments.Count()
                })
                .ToListAsync();

            return Ok(courses);
        }

        [HttpGet("{idCourse}/assignments")]
        public async Task<IActionResult> GetCourseAssignments(int idCourse, [FromQuery] bool publishedOnly = true)
        {
            var courseExists = await _context.Courses.AnyAsync(c => c.CourseId == idCourse);
            if (!courseExists) return NotFound($"Course with ID {idCourse} not found.");

            var query = _context.Assignments
                .AsNoTracking()
                .Where(a => a.CourseId == idCourse);

            if (publishedOnly)
            {
                query = query.Where(a => a.IsPublished == true);
            }

            var assignments = await query
                .Select(a => new AssignmentDto
                {
                    Id = a.AssignmentId,
                    Title = a.Title,
                    DueDate = a.DueDate,
                    MaxPoints = a.MaxPoints,
                    PublishedStatus = a.IsPublished,
                    SubmissionCount = a.Submissions.Count()
                })
                .ToListAsync();

            return Ok(assignments);
        }
    }
}