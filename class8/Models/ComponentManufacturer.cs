namespace APBD_TASK_7.Models
{
    public class ComponentManufacturer
    {
        public int Id { get; set; }
        public string Abbreviation { get; set; } = string.Empty; // nvarchar(30)
        public string FullName { get; set; } = string.Empty; // nvarchar(300)
        
        // Use DateOnly if using EF Core 8+, otherwise DateTime is fine for 'date'
        public DateTime FoundationDate { get; set; } 

        // Navigation property for the one-to-many relationship with Components
        public ICollection<Component> Components { get; set; } = new List<Component>();
    }
}