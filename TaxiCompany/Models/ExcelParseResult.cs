namespace TaxiCompany.Models;

public class ExcelParseResult
{
    public List<Location> Locations { get; set; }
    public List<Customer> Customers { get; set; }
    public List<Vehicle> Vehicles { get; set; }
    
    public ExcelParseResult(List<Location> locations, List<Customer> customers, List<Vehicle> vehicles)
    {
        this.Locations = locations;
        this.Customers = customers;
        this.Vehicles = vehicles;
    }
}