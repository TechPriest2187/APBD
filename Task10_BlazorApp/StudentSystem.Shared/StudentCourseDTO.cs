using System;
using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Shared;

public class StudentCourseDto
{
    [Required(ErrorMessage = "A valid student must be selected.")]
    public int StudentId { get; set; }

    [Required(ErrorMessage = "A valid course must be selected.")]
    public int CourseId { get; set; }

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
}