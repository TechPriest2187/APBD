namespace APBD_TASK_7.Models
{
    public class ComponentType
    {
        public int Id { get; set; }
        public string Abbreviation { get; set; } = string.Empty; // nvarchar(30)
        public string Name { get; set; } = string.Empty; // nvarchar(150)

        // Navigation property for the one-to-many relationship with Components
        public ICollection<Component> Components { get; set; } = new List<Component>();
    }
}