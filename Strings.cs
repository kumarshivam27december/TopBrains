interface IArea
{
    double GetArea();
}
abstract class Shape : IArea
{
    public abstract double GetArea();
}

class Cicle : Shape
{
    double radius;
    public Cicle(double radius)
    {
        this.radius = radius;
    }
    public override double GetArea()
    {
        return Math.PI*radius*radius;
    }
}

class Triangle : Shape
{
    double baseLength;
    double Height;
    public Triangle(double baseLength,double Height)
    {
        this.baseLength = baseLength;
        this.Height = Height;
    }
    public override double GetArea()
    {
        return 0.5*baseLength*Height;
    }
}

class Rectangle : Shape
{
    double length;
    double width;

    public Rectangle(double length,double width)
    {
        this.length = length;
        this.width = width;
    }
    public override double GetArea()
    {
        return length*width;
    }
}
public class Strings
{
    public static double ComputeTotalArea(string[] shapes)
    {
        double totalArea = 0;
        foreach (var shape in shapes)
        {
            var parts = shape.Split(' ');
            Shape shape1 = null;
            if (parts[0] == "C")
            {
                shape1 = new Shape(
                    Convert.ToDouble(parts[1])
                );
            }else if (parts[0] == "R")
            {
                shape1 = new Shape(
                    Convert.ToDouble(parts[1]),
                    Convert.ToDouble(parts[2])
                );
            }else if(parts[0] == "T")
            {
                shape1 = new Shape(
                    Convert.ToDouble(parts[1]),
                    Convert.ToDouble(parts[2])
                );
            }

            totalArea+=shape1.GetArea();
        }
        return Math.Round(totalArea,2,MidpointRounding.AwayFromZero);
    }
    public static void Main()
    {
        int n = Convert.ToInt32(Console.ReadLine());
        string[] shapes = new string[n];

        for (int i = 0; i < n; i++)
        {
            shapes[i] = Console.ReadLine();
        }

        Console.WriteLine(ComputeTotalArea(shapes));
    }
}