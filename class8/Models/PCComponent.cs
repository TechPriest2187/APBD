namespace APBD_TASK_7.Models
{
    public class PCComponent
    {
        // These two properties will form a Composite Primary Key (configured in DbContext)
        public int PCId { get; set; }
        public string ComponentCode { get; set; } = string.Empty;

        public int Amount { get; set; }

        // Navigation properties
        public PC PC { get; set; } = null!;
        public Component Component { get; set; } = null!;
    }
}