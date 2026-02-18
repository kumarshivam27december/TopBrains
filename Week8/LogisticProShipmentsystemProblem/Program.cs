public class Program
{
    public static void Main(string[] args)
    {
        ShipmentDetails ship = new ShipmentDetails();
        Console.WriteLine("Enter the shipment code");
        ship.ShipmentCode = Console.ReadLine() ?? "";
        if (ship.ValidateShipmentCode())
        {
            Console.WriteLine("Enter transport mode");
            ship.TransportMode = Console.ReadLine() ?? "";

            Console.WriteLine("Enter weight");
            ship.Weight = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter storage days");
            ship.StorageDays = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"The total shipping cost is {ship.CalculateTotalCost():F2}");

        }
        else
        {
            Console.WriteLine("Invalid shipment code");
        }
    }
}
class Shipment
{
    public string ShipmentCode { get; set; }
    public string TransportMode { get; set; }
    public double Weight { get; set; }

    public int StorageDays { get; set; }
}
class ShipmentDetails : Shipment
{
    public bool ValidateShipmentCode()
    {
        if (ShipmentCode.Length != 7)
        {
            return false;
        }
        string prefix = ShipmentCode.Substring(0, 3);
        if (prefix != "GC#")
        {
            return false;
        }
        string suffix = ShipmentCode.Substring(3, 5);
        try
        {
            int digit = Convert.ToInt32(suffix);
        }
        catch (FormatException)
        {

            return false;
        }
        return true;
    }
    public double CalculateTotalCost()
    {
        double RatePerKg = 0;
        if (TransportMode == "Sea")
        {
            RatePerKg = 15;
        }
        else if (TransportMode == "Air")
        {
            RatePerKg = 50;
        }
        else if (TransportMode == "Land")
        {
            RatePerKg = 25;
        }
        return Math.Round((Weight * RatePerKg + Math.Sqrt(StorageDays)), 2);
    }
}

