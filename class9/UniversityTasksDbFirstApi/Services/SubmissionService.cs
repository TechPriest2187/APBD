using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniversityTasksDbFirstApi.Data;
using UniversityTasksDbFirstApi.DTOs;
using UniversityTasksDbFirstApi.Models;

namespace UniversityTasksDbFirstApi.Services
{
    public class SubmissionService : ISubmissionService
    {
        private readonly UniversityTasksDbContext _context;

        public SubmissionService(UniversityTasksDbContext context)
        {
            _context = context;
        }

        public async Task<(int StatusCode, string ErrorMessage, int? CreatedId)> CreateSubmissionAsync(CreateSubmissionDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.RepositoryUrl) || !dto.RepositoryUrl.StartsWith("https://"))
            {
                return (400, "RepositoryUrl cannot be blank and must start with https://", null);
            }

            var student = await _context.Students.FindAsync(dto.StudentId);
            if (student == null) return (404, "Student not found.", null);
            if (!student.IsActive) return (400, "Student is not active.", null);

            var assignment = await _context.Assignments.FindAsync(dto.AssignmentId);
            if (assignment == null) return (404, "Assignment not found.", null);
            if (!assignment.IsPublished) return (400, "Assignment is not published.", null);

            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.StudentId == dto.StudentId && e.CourseId == assignment.CourseId);
            
            if (enrollment == null || (enrollment.Status != "Active" && enrollment.Status != "Completed"))
            {
                return (400, "Student is not enrolled in the course, or enrollment is not Active/Completed.", null);
            }

            var existingSubmission = await _context.Submissions
                .AnyAsync(s => s.StudentId == dto.StudentId && s.AssignmentId == dto.AssignmentId);
            
            if (existingSubmission)
            {
                return (409, "Student has already submitted this assignment.", null);
            }

            var submission = new Submission
            {
                StudentId = dto.StudentId,
                AssignmentId = dto.AssignmentId,
                RepositoryUrl = dto.RepositoryUrl,
                Status = assignment.IsOverdue(DateTime.Now) ? "Late" : "Submitted",
            };

            _context.Submissions.Add(submission);
            await _context.SaveChangesAsync();

            return (201, string.Empty, submission.SubmissionId);
        }

        public async Task<(int StatusCode, string ErrorMessage)> GradeSubmissionAsync(int submissionId, GradeSubmissionDto dto)
        {
            var submission = await _context.Submissions
                .Include(s => s.Assignment)
                .FirstOrDefaultAsync(s => s.SubmissionId == submissionId);

            if (submission == null) return (404, "Submission not found.");

            if (dto.Score < 0) return (400, "Score cannot be lower than 0.");
            if (dto.Score > submission.Assignment.MaxPoints)
            {
                return (400, $"Score cannot be higher than the assignment's MaxPoints ({submission.Assignment.MaxPoints}).");
            }

            submission.Score = dto.Score;
            submission.Feedback = dto.Feedback;
            submission.Status = "Graded";

            await _context.SaveChangesAsync();

            return (200, string.Empty);
        }

        public async Task<(int StatusCode, string ErrorMessage)> DeleteSubmissionAsync(int submissionId)
        {
            var submission = await _context.Submissions.FindAsync(submissionId);
            
            if (submission == null) return (404, "Submission not found.");

            if (submission.Status == "Graded")
            {
                return (400, "A graded submission cannot be deleted.");
            }

            _context.Submissions.Remove(submission);
            await _context.SaveChangesAsync();

            return (204, string.Empty);
        }
    }
}