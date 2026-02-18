abstract class Consultant
{
    public string Id { get; set; }
    public string Name { get; set; }

    public double PayoutAmount { get; set; }
    public bool ValidateConsultantId()
    {
        if (Id.Length == 6)
        {
            string temp = Id.Substring(0,2);
            if (temp != "DR")
            {
                return false;
            }
            else
            {
                try
                {
                    string sub = Id.Substring(2, 4);
                    int num = Convert.ToInt32(sub);
                    return true;
                }
                catch (FormatException)
                {
                    return false;
                }
            }
                
            }
        return false;
    }
    public abstract Consultant CalculateGrossPayout();
}

class InHouse : Consultant
{
    public double MonthlyStipend { get; set; }
    public override Consultant CalculateGrossPayout()
    {
        double allowance = 0.20 * MonthlyStipend;
        double bonus = 0.10 * MonthlyStipend;
        this.PayoutAmount = MonthlyStipend + allowance + bonus;
        return this;
    }
}
class Visiting : Consultant
{
    public int ConsultationCount { get; set; }
    public double ratePervisit { get; set; }


    public override Consultant CalculateGrossPayout()
    {
        this.PayoutAmount = ConsultationCount * ratePervisit;
        return this;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Enter the type");
            string type = Console.ReadLine() ?? "";
            Consultant doctor;
            if (type == "InHouse")
            {
                Console.WriteLine("Enter Id");
                string Id = Console.ReadLine() ?? "";
                Console.WriteLine("Enter the monthly stipend");
                double monthlyStipend = Convert.ToDouble(Console.ReadLine());

                doctor = new InHouse
                {
                  Id=Id,
                  Name="ravi",
                  MonthlyStipend =monthlyStipend  
                };

                if (!doctor.ValidateConsultantId())
                {
                    Console.WriteLine("Invalid Doctor ID");
                }
                double tdsApplied = 0;
                if (monthlyStipend < 5000)
                {
                    tdsApplied = 5;
                }
                else
                {
                    tdsApplied = 15;
                }
                doctor.CalculateGrossPayout();
                Console.WriteLine($"Gross: {monthlyStipend} | TDS Applied: {tdsApplied}% | Net Payout: {doctor.PayoutAmount}");

            }else if(type == "Visiting")
            {
                Console.WriteLine("Enter Id");
                string Id = Console.ReadLine() ?? "";
                Console.WriteLine("Enter no of visits");
                int noOfVisit = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter rate per visit");
                int ratePervisit = Convert.ToInt32(Console.ReadLine());
                double tdsApplied = 10;
                doctor = new Visiting
                {
                    Id = Id,
                    Name = "Shivam",
                    ConsultationCount = noOfVisit,
                    ratePervisit = ratePervisit
                };

                if (!doctor.ValidateConsultantId())
                {
                    Console.WriteLine("Invalid Doctor ID");
                }
                
                Console.WriteLine($"Gross: {doctor.CalculateGrossPayout()} || TDS  Applied : {tdsApplied}% | Net Payout: {doctor.PayoutAmount} ");
            }
            else
            {
                Console.WriteLine("Invalid Consultant type");
                return;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }
}