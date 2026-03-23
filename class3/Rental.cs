namespace class3
{
    public class Rental
    {
        public int Id { get; set; }
        public Equipment RentedEquipment { get; set; }
        public User Renter { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime DueDate = RentalDate.AddDays(14); // Assuming a 2-week rental period
        public DateTime? ReturnDate { get; set; }

        public Rental(int id, Equipment equipment, User renter, DateTime rentalDate)
        {
            Id = id;
            RentedEquipment = equipment;
            equipment.Status = EquipmentStatus.Rented;
            Renter = renter;
            RentalDate = rentalDate;
            ReturnDate = null;
        }

        public void ReturnEquipment(DateTime returnDate)
        {
            ReturnDate = returnDate;
            RentedEquipment.Status = EquipmentStatus.Available;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Rental ID: {Id}, Equipment: {RentedEquipment.Name}, Rental Date: {RentalDate}, Return Date: {(ReturnDate.HasValue ? ReturnDate.Value.ToString() : "Not returned")}");
        }
    }
}