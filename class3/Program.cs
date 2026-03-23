using class3;

string[] args = Console.ReadLine().Split(' ');

List<Equipment> equipments = new List<Equipment>();
List<User> users = new List<User>();
List<Rental> rentals = new List<Rental>();

switch (args[0])
{
    case "User":
        var user = new User(args[1], args[2], args[3]);
        users.Add(user);
        user.DisplayInfo();
        break;
    case "Laptop":
        var laptop = new Laptop(args[1], args[2], args[3], int.Parse(args[4]), int.Parse(args[5]));;
        equipments.Add(laptop);
        laptop.DisplayInfo();
        break;
    case "Projector":
        var projector = new Projector(args[1], args[2], int.Parse(args[3]), args[4]);
        equipments.Add(projector);
        projector.DisplayInfo();
        break;
    case "Camera":
        var camera = new Camera(args[1], args[2], int.Parse(args[3]), args[4]);
        equipments.Add(camera);
        camera.DisplayInfo();
        break;
    case "Status":
        for(int i = 0; i < equipments.Count; i++){equipments[i].DisplayInfo();}
        break;
    case "Rent":
        equipmentToRent = equipments[int.Parse(args[2])];
        renter = users[int.Parse(args[1])];
        if (equipmentToRent.Status != EquipmentStatus.Available)
        {
            Console.WriteLine($"Equipment {equipmentToRent.Name} is not available for rent.");
            break;
        }
        if (rentals.Count(r => r.Renter.Id == renter.Id && !r.ReturnDate.HasValue) >= renter.MaxActiveRentals)
        {
            Console.WriteLine($"User {renter.Name} has reached the maximum number of active rentals.");
            break;
        }
        var rental = new Rental(equipmentToRent, 
        renter, DateTime.Now, DateTime.Now.AddDays(7));
        rentals.Add(rental);
        Console.WriteLine($"User {renter.Name} rented {equipmentToRent.Name}");
        break;
    default:
        Console.WriteLine("Unknown equipment type.");
        break;
}