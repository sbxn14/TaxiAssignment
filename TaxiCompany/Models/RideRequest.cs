namespace TaxiCompany.Models;

public class RideRequest
{
    public Company Company { get; set; }
    public List<Customer> Customers { get; set; }
    public Location PickUpLocation { get; set; }
    public Location DropOffLocation { get; set; }
    public List<Location> Destinations { get; set; }
}