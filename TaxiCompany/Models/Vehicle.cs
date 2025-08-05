namespace TaxiCompany.Models;

public class Vehicle
{
    public string Name { get; set; }
    public int CustomerCapacity { get; set; }
    public int WheelchairCapacity { get; set; }

    public Vehicle(string name, int customerCapacity, int wheelchairCapacity)
    {
        this.Name = name;
        this.CustomerCapacity = customerCapacity;
        this.WheelchairCapacity = wheelchairCapacity;
    }
}