using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Shared;
public class StudentDto
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Index number is required.")]
    public string IndexNumber { get; set; } = string.Empty;

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Range(1, 8, ErrorMessage = "Semester must be between 1 and 8.")]
    public int Semester { get; set; }
}