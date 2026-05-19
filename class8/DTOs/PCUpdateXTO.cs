using System.ComponentModel.DataAnnotations;

namespace APBD_TASK_7.DTOs
{
    public class PcUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public double Weight { get; set; }

        [Required]
        public int Warranty { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int Stock { get; set; }
    }
}