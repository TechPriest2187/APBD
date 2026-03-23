namespace class3
{
    public class Camera : Equipment
    {
        public int Megapixels { get; set; }
        public string LensType { get; set; }

        public Camera(string name, string description, int megapixels, string lensType)
            : base(name, description)
        {
            Megapixels = megapixels;
            LensType = lensType;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Megapixels: {Megapixels}, Lens Type: {LensType}");
        }
    }
}