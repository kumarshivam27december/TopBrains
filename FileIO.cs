using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string inputFile = "log.txt";
        string outputFile = "error.txt";

        if (!File.Exists(inputFile))
        {
            Console.WriteLine("log.txt file not found.");
            return;
        }
        using (StreamReader reader = new StreamReader(inputFile))
        using (StreamWriter writer = new StreamWriter(outputFile))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains("ERROR"))
                {
                    writer.WriteLine(line);
                }
            }
        }
        Console.WriteLine("ERROR logs extracted successfully.");
    }
}
