public class MidPointRounding
{
    static void Main()
    {
        double radius = double.Parse(Console.ReadLine());
        double area = CalculateCircleArea(radius);
        Console.WriteLine(area);
    }

    static double CalculateCircleArea(double radius)
    {
        double area = Math.PI * radius * radius;
        return Math.Round(area, 2, MidpointRounding.AwayFromZero);
    }
}