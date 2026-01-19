
class Programming
{
    public static bool isprime(int num)
    {
        if (num <= 1)
        {
            return false;
        }
        if (num == 2)
        {
            return true;
        }
        if (num == 3)
        {
            return true;
        }

        for(int i = 2; i < num; i++)
        {
            if (num % i == 0)
            {
                return false;
            }
        }
        return true;
    }

    public static int GetSum(int input)
    {
        int sum = 0;
        while (input != 0)
        {
            sum+=(input%10);
            input/=10;
        }
        return sum;
    }
    public static void Main(string[] args)
    {

        int StartNumber = Convert.ToInt32(Console.ReadLine());
        int EndNumber = Convert.ToInt32(Console.ReadLine());
        int totalCount = 0;

        for (int i = StartNumber; i <= EndNumber; i++)
        {
            int originalNumber = i;
            if (!isprime(originalNumber))
            {
                int sumoforiginal = GetSum(originalNumber);
                int originalSquared = originalNumber * originalNumber;
                int originalSquaredSum = GetSum(originalSquared);
                int squareOfSumofOriginal = sumoforiginal*sumoforiginal;
                if (originalSquaredSum == squareOfSumofOriginal)
                {
                    totalCount++;
                }
            }

        }

        Console.WriteLine(totalCount);

    }
}