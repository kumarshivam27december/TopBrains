public class TimeConversion
{
    public static void Main(string[] args)
    {
        int totalSecond = Convert.ToInt32(Console.ReadLine());
        string str = "";
        if (totalSecond < 60)
        {
            str+=$"0:{totalSecond}";
            Console.WriteLine(str);
            return;
        }

        int minute = totalSecond/60;
        str+=minute;
        str+=":";
        int second = totalSecond%60;
        if (second < 10)
        {
            str+=$"0{second}";
        }
        else
        {
            str+=$"{second}";
        }
        Console.WriteLine(str);
    }
}