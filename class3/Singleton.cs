namespace class3
{
    public class Singleton
    {
        private static Singleton _instance;

        public static Singleton Instance
        {
            get
            {
                _instance ??= new Singleton();
                return _instance;
            }
        }

        private Singleton()
        {
            // Private constructor to prevent instantiation
        }

        public void SomeMethod()
        {
            Console.WriteLine("Method called on singleton instance.");
        }
    }
}