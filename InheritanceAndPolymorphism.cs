public class InheritanceAndPolymorphism
{
    public static decimal ComputeTotalPayroll(string[] employees)
    {
        decimal totalPay = 0;
        foreach (var emp in employees)
        {
            var parts = emp.Split(' ');
            Employee employee = null;
            if (parts[0] == "H")
            {
                employee = new HourlyEmployee(
                    Convert.ToDecimal(parts[1]),
                    Convert.ToDecimal(parts[2])
                );
            }else if (parts[0] == "S")
            {
                employee = new SalariedEmployee(
                    Convert.ToDecimal(parts[1])
                );
            }else if (parts[0] == "C")
            {
                employee = new CommissionEmployee(
                    Convert.ToDecimal(parts[1]),
                    Convert.ToDecimal(parts[2])
                );
            }

            totalPay+=employee.GetPay();
        }
        return Math.Round(totalPay,2);
    }
    public static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine());
        string[] employees = new string[n];

        for (int i = 0; i < n; i++)
        {
            employees[i] = Console.ReadLine();
        }

        Console.WriteLine(ComputeTotalPayroll(employees));
    }
}

abstract class Employee{
    public abstract decimal GetPay();
}

class HourlyEmployee : Employee
{
    private decimal rate;
    private decimal hours;

    public HourlyEmployee(decimal rate,decimal hours)
    {
        this.rate = rate;
        this.hours = hours;
    }
    public override decimal GetPay()
    {
        return rate*hours;
    }
}

class SalariedEmployee : Employee
{
    private decimal monthlySalary;
    public SalariedEmployee(decimal monthlySalary)
    {
        this.monthlySalary = monthlySalary;
    }
    public override decimal GetPay()
    {
        return monthlySalary;
    }
}

class CommissionEmployee : Employee
{
    private decimal commission;
    private decimal baseSalary;
    
    public CommissionEmployee(decimal commission,decimal baseSalary)
    {
        this.commission = commission;
        this.baseSalary = baseSalary;
    }

    public override decimal GetPay()
    {
        return baseSalary*commission;
    }
}

