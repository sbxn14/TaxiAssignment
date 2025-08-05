namespace TaxiCompany.Models;

public class Customer
{
    public string Name { get; set; }
    public bool UsesWheelchair { get; set; }

    public Customer(string name, bool usesWheelchair)
    {
        this.Name = name;
        this.UsesWheelchair = usesWheelchair;
    }
}