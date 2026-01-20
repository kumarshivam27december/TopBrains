public class ArithmeticExpression
{
    public static void Main(string[] args)
    {
        string input = Console.ReadLine() ?? "";
        try
        {
            string[] inputarr = input.Split();
            int number1 = Convert.ToInt32(inputarr[0]);
            string operation = inputarr[1];
            int number2 = Convert.ToInt32(inputarr[2]);
            string output = "";
            if (operation == "+")
            {
                output += (number1 + number2);
            }
            else if (operation == "-")
            {
                output += (number1 - number2);
            }
            else if (operation == "*")
            {
                output += (number1 * number2);
            }
            else if (operation == "/")
            {
                if (number2 == 0)
                {
                    throw new DivideByZeroException("Error:DivideByZero");
                }
                output += (number1 / number2);
            }
            else
            {
                throw new UnknowOperation("Error:UnknownOperator");
            }
        }
        catch (UnknowOperation)
        {

        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine(ex.Message);
        }catch(FormatException)
        {
            Console.WriteLine("Error:InvalidNumber");
        }
        catch (Exception)
        {
            Console.WriteLine("Error:InvalidExpression");
        }

        Console.WriteLine(output);
}
}

public class UnknowOperation : System.Exception
{
    public UnknowOperation(string str)
    {
        Console.WriteLine(str);
    }

    public UnknowOperation()
    {
        Console.WriteLine("Error:UnknownOperator");
    }
}