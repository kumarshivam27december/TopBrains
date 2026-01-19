public class Conversion
{
    public static void Main(string[] args)
    {
        int inputFoot = Convert.ToInt32(Console.ReadLine());
        double convertToCentimeter = ConvertToCentimeter(inputFoot);
        Console.WriteLine(convertToCentimeter);
    }
    public static double ConvertToCentimeter(int inputFoot)
    {
        double result = 30.48*inputFoot;
        return Math.Round(result,2,MidpointRounding.AwayFromZero);
    }
}