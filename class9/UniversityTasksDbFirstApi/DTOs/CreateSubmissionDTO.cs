using System.ComponentModel.DataAnnotations;

namespace UniversityTasksDbFirstApi.DTOs
{
    public class CreateSubmissionDto
    {
        [Required]
        public int AssignmentId { get; set; }
        
        [Required]
        public int StudentId { get; set; }
        
        [Required]
        [Url(ErrorMessage = "Invalid URL format.")]
        public string RepositoryUrl { get; set; } = string.Empty;
    }
}