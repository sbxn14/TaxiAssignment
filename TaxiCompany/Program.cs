using TaxiCompany.Models;

namespace TaxiCompany;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        Console.WriteLine();
        
        TaxiLogic.Run();
    }
}