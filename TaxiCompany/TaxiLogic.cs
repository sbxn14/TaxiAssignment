using TaxiCompany.Models;

namespace TaxiCompany;

public class TaxiLogic
{
    public static void Run()
    {
        var result = ExcelReader.ReadExcel();
        
        //Taxibedrijf De Kruik
        var company = new Company
        {
            Name = "De Kruik",
            Vehicles = result.Vehicles
        };

        // Wilhelminastraat
        var pickUp = result.Locations[0];
        var dropOff = pickUp;

        var destinations = result.Locations;
        destinations.RemoveAt(0);

        var optimisedDestinations = GetOptimisedRoute(destinations, pickUp, dropOff);

        var rideRequest = new RideRequest
        {
            Company = company,
            Customers = result.Customers,
            PickUpLocation = pickUp,
            DropOffLocation = dropOff,
            Destinations = optimisedDestinations
        };

        var vehicles = GetNeededVehicles(rideRequest, result.Vehicles);
        
        Console.WriteLine();
        Console.WriteLine("We're using the following vehicles:");
        foreach (var v in vehicles)
        {
            Console.WriteLine($"{v.Name} - Capacity: {v.CustomerCapacity}, Wheelchair Capacity: {v.WheelchairCapacity}");
        }
        Console.WriteLine();

        var distance = CalculateTotalDistance(rideRequest);

        // Ik neem een gemiddelde snelheid van 35 km/h
        var timeInHours = distance / 35;
        var timeInMinutes = timeInHours * 60;
        var roundedTimeInMinutes = Math.Ceiling(timeInMinutes);
        
        Console.WriteLine();
        Console.WriteLine(
            $"Total travel time at avg 35km/h: {roundedTimeInMinutes} minutes. (or {Math.Round(timeInHours, 2)} hours)");
    }

    private static List<Vehicle> GetNeededVehicles(RideRequest rideRequest, List<Vehicle> vehicles)
    {
        var amountCustomers = rideRequest.Customers.Count;
        var assignedCustomers = 0;
        var amountWheelchairs = rideRequest.Customers.Count(x => x.UsesWheelchair);
        var assignedWheelchairs = 0;
        
        var neededVehicles = new List<Vehicle>();
        
        var sortedVehicles = vehicles
            .OrderByDescending(v => v.WheelchairCapacity)
            .ThenByDescending(v => v.CustomerCapacity)
            .ToList();

        foreach (var vehicle in sortedVehicles)
        {
            if (assignedCustomers >= amountCustomers && assignedWheelchairs >= amountWheelchairs)
            {
                break;
            }
            
            neededVehicles.Add(vehicle);
            assignedCustomers += vehicle.CustomerCapacity;
            assignedWheelchairs += vehicle.WheelchairCapacity;
        }

        return neededVehicles;
    }

    private static double CalculateTotalDistance(RideRequest rideRequest)
    {
        var totalDistance = 0.0;
        
        for (var i = 0; i < rideRequest.Destinations.Count - 1; i++)
        {
            var destination = rideRequest.Destinations[i];
            var nextDestination = rideRequest.Destinations[i + 1];

            var distanceInKm = destination.Coordinates.DistanceTo(nextDestination.Coordinates) * 111;
            Console.WriteLine(
                $"Total distance for {destination.Name} to {nextDestination.Name}: {Math.Round(distanceInKm, 2)} km.");
            totalDistance += distanceInKm;
        }
        
        Console.WriteLine();
        Console.WriteLine($"Total distance: {Math.Round(totalDistance, 2)} km.");
        return totalDistance;
    }

    private static List<Location> GetOptimisedRoute(List<Location> destinations, Location pickUp, Location dropOff)
    {
        var route = new List<Location> { pickUp };
        var toVisit = new List<Location>(destinations);

        var current = pickUp;

        for (var i = 0; i < destinations.Count; i++)
        {
            var nearestDistance = double.MaxValue;
            Location? nearest = null;

            foreach (var destination in toVisit)
            {
                var distance = current.Coordinates.DistanceTo(destination.Coordinates);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearest = destination;
                }
            }

            if (nearest != null)
            {
                route.Add(nearest);
                toVisit.Remove(nearest);
                current = nearest;
            }
        }

        route.Add(dropOff);

        return route;
    }
}