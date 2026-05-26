using System.Collections.Generic;

namespace UniversityTasksDbFirstApi.DTOs
{
    public class StudentDashboardDto
    {
        public int Id { get; set; }
        public string IndexNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public bool ActiveStatus { get; set; }
        
        public List<StudentEnrollmentDto> Enrollments { get; set; } = new();
        public List<SubmissionDto> Submissions { get; set; } = new();
    }

    public class StudentEnrollmentDto
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}