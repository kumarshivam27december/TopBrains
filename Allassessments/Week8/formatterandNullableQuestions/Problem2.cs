using System;
using System.Collections.Generic;

public interface IStringParser
{
    float[] Parse(string input);
}

public interface IRounder
{
    float Round(float value);
}

public class SensorDataNormalizer : IStringParser, IRounder
{
    public float[] Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return null;

        string[] parts = input.Split(',');
        List<float> cleanedValues = new List<float>();

        foreach (string part in parts)
        {
            string value = part.Trim();

            // Ignore empty, null text, NaN
            if (string.IsNullOrWhiteSpace(value) ||
                value.Equals("null", StringComparison.OrdinalIgnoreCase) ||
                value.Equals("NaN", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            // Safe numeric conversion
            if (float.TryParse(value, out float number))
            {
                float rounded = Round(number);
                cleanedValues.Add(rounded);
            }
        }

        if (cleanedValues.Count == 0)
            return null;

        return cleanedValues.ToArray();
    }

    public float Round(float value)
    {
        return (float)Math.Round(value, 2);
    }
}
class Problem2
{
    public static void Solve()
    {
        string input = " 24.5678, 18.9, null, , 31.0049, error, 29, 17.999, NaN ";

        IStringParser normalizer = new SensorDataNormalizer();

        float[] result = normalizer.Parse(input);

        if (result == null)
        {
            Console.WriteLine("No valid data found.");
        }
        else
        {
            Console.WriteLine("{ " + string.Join(", ", result) + " }");
        }
    }
}
