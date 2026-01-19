public class DisplayHeight
{
    public static void Main(string[] args)
    {
        int heightInCentimeter = Convert.ToInt32(Console.ReadLine());
        string answer;
        if (heightInCentimeter < 150)
        {
            answer = "Short";
        }else if(heightInCentimeter>=150 && heightInCentimeter < 180)
        {
            answer = "Average";
        }
        else
        {
            answer = "Tall";
        }

        Console.WriteLine(answer);
    }
}