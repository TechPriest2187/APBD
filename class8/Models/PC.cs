
namespace APBD_TASK_7.Models
{
    public class PC
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // nvarchar(50)
        public double Weight { get; set; } // float(5)
        public int Warranty { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Stock { get; set; }

        // Navigation property for the one-to-many relationship with PCComponents
        public ICollection<PCComponent> PCComponents { get; set; } = new List<PCComponent>();
    }
}