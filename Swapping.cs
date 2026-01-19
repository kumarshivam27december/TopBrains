public class Swapping
{
    public static void Main(string[] args)
    {
        int input1 = Convert.ToInt32(Console.ReadLine());
        int input2 = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine($"{input1} {input2}");
        SwapUsingRef(input1,input2);
        Console.WriteLine($"{input1} {input2}");
        SwapUsingOut(input1,input2);
        Console.WriteLine($"{input1} {input2}");
    }
    public static void SwapUsingRef(ref int input1,ref int input2)
    {
        input1 = input1 + input2;
        input2 = input1-input2;
        input1 = input1 - input2;
    }

    public static void SwapUsingOut(int input1,int input2,out int x ,out int y)
    {
        x = input2;
        y = input1;
    }

}