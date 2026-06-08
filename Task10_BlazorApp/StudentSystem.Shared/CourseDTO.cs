using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Shared;

public class CourseDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Course name is required.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "ECTS points are required.")]
    [Range(1, 30, ErrorMessage = "ECTS points must be a valid number.")]
    public int Ects { get; set; }
}