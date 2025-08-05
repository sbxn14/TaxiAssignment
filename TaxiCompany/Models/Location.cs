namespace TaxiCompany.Models;

public class Location
{
    public string Name { get; set; }
    public Coordinates Coordinates { get; set; }

    public Location(string name, Coordinates coordinates)
    {
        this.Name = name;
        this.Coordinates = coordinates;
    }
}