namespace class3
{
    private static int _nextId = 1;

    public int Id { get; private set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public UserTypeEnum Type { get; set; }
    public User(string name, string email, UserTypeEnum type)
    {
        Id = _nextId++;
        Name = name;
        Email = email;
        Type = type;
    }

    public int MaxActiveRentals => Type switch
    {
        UserTypeEnum.Student => 2,
        UserTypeEnum.Employee => 5,
        _ => 0
    };
}