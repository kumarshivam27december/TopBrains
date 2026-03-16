using System;

class Problem1
{
    public static void Solve()
    {
        // Convert int Student ID to string
        int studentId = 105;
        string studentIdStr = studentId.ToString();
        Console.WriteLine("Student ID: " + studentIdStr);

        //Convert string "45" to int
        string seatsStr = "45";
        int seats = Convert.ToInt32(seatsStr);
        Console.WriteLine("Available Seats: " + seats);

        //Convert string course fee to decimal
        string feeStr = "15000.50";
        decimal courseFee = Convert.ToDecimal(feeStr);
        Console.WriteLine("Course Fee: " + courseFee);

        //Convert int discount to double
        int discount = 15;
        double discountRate = Convert.ToDouble(discount);
        Console.WriteLine("Discount Rate: " + discountRate);

        //Convert float attendance to double
        float attendance = 92.75f;
        double attendanceDouble = attendance; // implicit conversion
        Console.WriteLine("Attendance (double): " + attendanceDouble);

        //Convert double duration to int
        double duration = 6.8;
        int days = (int)duration; // explicit casting (truncates decimal)
        Console.WriteLine("Number of Days: " + days);

        //Convert double temperature to float
        double temperature = 37.45678;
        float tempFloat = (float)temperature; // explicit casting
        Console.WriteLine("Temperature (float): " + tempFloat);

        //Convert decimal total amount to string formatted to 2 decimal places
        decimal totalAmount = 12345.6789m;
        string formattedAmount = totalAmount.ToString("F2");
        Console.WriteLine("Total Amount: " + formattedAmount);

        //Convert character grade to numeric value
        char grade = 'B';
        int gradeValue = (int)grade;
        Console.WriteLine("Grade Numeric Value: " + gradeValue);

        //nvert boolean false to string
        bool status = false;
        string statusStr = status.ToString();
        Console.WriteLine("Status: " + statusStr);
    }
}
