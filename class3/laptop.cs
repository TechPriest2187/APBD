namespace class3
{
    public class Laptop : Equipment
    {
        public string Processor { get; set; }
        public int RAM { get; set; } // in GB
        public int Storage { get; set; } // in GB

        public Laptop(string name, string description, string processor, int ram, int storage)
            : base(name, description)
        {
            Processor = processor;
            RAM = ram;
            Storage = storage;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Processor: {Processor}, RAM: {RAM}GB, Storage: {Storage}GB");
        }
    }
}