namespace BiddingApp.Infrastructure.Dtos.VehicleDtos
{
    public class VehicleVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brands { get; set; }
        public string VIN { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string ImageUrl { get; set; }
        public int NumberOfChairs { get; set; }
        public int Horsepower { get; set; }
        public decimal MaximumSpeed { get; set; }
        public decimal TrunkCapacity { get; set; }
        public string Status { get; set; }
    }
}
