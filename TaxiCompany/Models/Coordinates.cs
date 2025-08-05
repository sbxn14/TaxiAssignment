namespace TaxiCompany.Models;

public class Coordinates
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public Coordinates(double latitude, double longitude)
    {
        this.Latitude = latitude;
        this.Longitude = longitude;
    }

    public double DistanceTo(Coordinates other)
    {
        // Er zijn natuurlijk verschillende manieren om dit te doen.
        // Ik heb ervoor gekozen om de afstanden te berekenen op basis van de "Euclidische afstand":
        // https://en.wikipedia.org/wiki/Euclidean_distance
        // Voor kleine afstanden (zoals binnen Tilburg) is dit accuraat genoeg.
        //
        // Ik heb hiervoor gekozen in plaats van bijvoorbeeld een API zoals Google’s Distance Matrix API,
        // of een methode die op basis van de "Haversine formula" de kortste route bepaalt, waarbij ook rekening wordt gehouden
        // met de kromming van de aarde.
        // In deze implementatie wordt daar nu geen rekening mee gehouden.
        // Zo beperk ik pragmatisch de scope van dit assignment,
        // in een daadwerkelijke implementatie zouden de andere opties in de meeste gevallen beter zijn.

        var differenceX = Longitude - other.Longitude;
        var differenceY = Latitude - other.Latitude;

        return Math.Sqrt(differenceX * differenceX + differenceY * differenceY);
    }
}