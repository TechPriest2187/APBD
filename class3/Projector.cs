namespace class3
{
    public class Projector : Equipment
    {
        public int ScreenWidth { get; set; }
        public string ScreenLength { get; set; }

        public Projector(string name, string description, int width, string length)
            : base(name, description)
        {
            ScreenWidth = width;
            ScreenLength = length;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Width: {ScreenWidth}, Length: {ScreenLength}");
        }
    }
}