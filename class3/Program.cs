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
    case "Return":
        var rentalToReturn = rentals[int.Parse(args[1])];
        if (rentalToReturn.ReturnDate.HasValue)
        {
            Console.WriteLine($"Rental ID {rentalToReturn.Id} has already been returned.");
            break;
        }
        if(DateTime.Now > rentalToReturn.DueDate)
        {
            Console.WriteLine($"Rental ID {rentalToReturn.Id} is overdue. Due date was: {rentalToReturn.DueDate}");
            Console.WriteLine($"Penalty is: ${DateTime.Now.Subtract(rentalToReturn.DueDate).Days * 5} dollars.");
        }
        rentalToReturn.ReturnEquipment(DateTime.Now);
        Console.WriteLine($"Rental ID {rentalToReturn.Id} has been returned.");
        break;
    case "MarkUnavailable":
        var equipmentToMark = equipments[int.Parse(args[1])];
        equipmentToMark.Status = EquipmentStatus.Unavailable;
        Console.WriteLine($"Equipment {equipmentToMark.Name} marked as unavailable.");
        break;
    case "UserRentals":
        var userForRentals = users[int.Parse(args[1])];
        var userRentals = rentals.Where(r => r.Renter.Id == userForRentals.Id);
        Console.WriteLine($"Rentals for user {userForRentals.Name}:");
        foreach (var r in userRentals){r.DisplayInfo();}
        break;
    case "OverdueRentals":
        Console.WriteLine("Overdue rentals:");
        foreach (var r in rentals){r.checkForOverdue();}
        break;
    case "Report":
        Console.WriteLine("Rental Report:");
        foreach (var r in rentals)
        {
            r.DisplayInfo();
            if (!r.ReturnDate.HasValue && DateTime.Now > r.DueDate)
            {
                Console.WriteLine($"  Status: Overdue (Due date was: {r.DueDate})");
            }
            else if (!r.ReturnDate.HasValue)
            {
                Console.WriteLine("  Status: Currently rented");
            }
            else
            {
                Console.WriteLine("  Status: Returned");
            }
        }
        break;
    default:
        Console.WriteLine("Unknown equipment type.");
        break;
}