using ClosedXML.Excel;
using TaxiCompany.Models;

namespace TaxiCompany;

public class ExcelReader
{
    public static ExcelParseResult ReadExcel()
    {
        // this method will be very hardcoded towards the provided excel file
        // in practice you'd make a standardized format for any excel files
        // and make a method that works dynamically
        // I've chosen to not use the provided Distance and Traveltime tables
        // in favour of writing my own implementation to calculate those values.
        var path = Path.Combine(AppContext.BaseDirectory, "casus.xlsx");
        var wb = new XLWorkbook(path);
        var ws = wb.Worksheet(1);
        
        Console.WriteLine("Locations to Visit: ");
        var locations = new List<Location>();
        for (var row = 3; row <= 12; row++)
        {
            var name = ws.Cell(row, 2).GetString();
            var lat = double.Parse(ws.Cell(row, 3).GetString());
            var lon = double.Parse(ws.Cell(row, 4).GetString());
            
            locations.Add(new Location(name, new Coordinates(lat, lon)));
            Console.WriteLine(name);
        }

        var customers = new List<Customer>();
        for (var row = 16; row <= 25; row++)
        {
            var name = ws.Cell(row, 2).GetString();
            var wheelchair = ws.Cell(row, 3).GetString().ToLower() == "ja";
            customers.Add(new Customer(name, wheelchair));
        }

        var vehicles = new List<Vehicle>();
        for (var row = 29; row <= 31; row++)
        {
            var name = ws.Cell(row, 2).GetString();
            var capacity = ws.Cell(row, 3).GetValue<int>();
            var wheelchairCapacity = ws.Cell(row, 4).GetValue<int>();
            vehicles.Add(new Vehicle(name, capacity, wheelchairCapacity));
        }
        
        return new ExcelParseResult(locations, customers, vehicles);
    }
}