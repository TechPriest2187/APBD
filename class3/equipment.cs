using EquipmentStatus = class3.EquipmentStatus;

public abstract class Equipment
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }

    public EquipmentStatus Status { get; set; } = EquipmentStatus.Available;

    public DateTime AddedDate { get; set; }

    Equipment(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public abstract virtual void DisplayInfo()
    {
        Console.WriteLine($"Equipment: {Name}, Id: {Id}");
    }
}