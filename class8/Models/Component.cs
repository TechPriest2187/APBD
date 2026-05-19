namespace APBD_TASK_7.Models
{
    public class Component
    {
        public string Code { get; set; } = string.Empty; // char(10), Primary Key
        public string Name { get; set; } = string.Empty; // nvarchar(300)
        public string Description { get; set; } = string.Empty; // nvarchar(max)

        // Foreign Keys
        public int ComponentManufacturersId { get; set; }
        public int ComponentTypesId { get; set; }

        // Navigation properties linking back to parent tables
        public ComponentManufacturer ComponentManufacturer { get; set; } = null!;
        public ComponentType ComponentType { get; set; } = null!;

        // Navigation property for the many-to-many mapping table
        public ICollection<PCComponent> PCComponents { get; set; } = new List<PCComponent>();
    }
}