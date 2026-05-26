using System.ComponentModel.DataAnnotations;

namespace UniversityTasksDbFirstApi.DTOs
{
    public class GradeSubmissionDto
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Score cannot be lower than 0.")]
        public int Score { get; set; }
        
        public string? Feedback { get; set; }
    }
}