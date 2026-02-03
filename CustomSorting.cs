public class CustomSorting
{
    public static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine() ?? "0");
        List<Student> students = new List<Student>();

        for (int i = 0; i < n; i++)
        {
            string[] input = (Console.ReadLine() ?? "").Split(' ');

            string name = input[0];
            int age = Convert.ToInt32(input[1]);
            int marks = Convert.ToInt32(input[2]);

            students.Add(new Student(name, age, marks));
        }

        students.Sort(new StudentComparer());

        foreach (var s in students)
        {
            Console.WriteLine($"{s.Name} {s.Age} {s.Marks}");
        }
    }
}

public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public int Marks { get; set; }

    public Student(string name, int age, int marks)
    {
        Name = name;
        Age = age;
        Marks = marks;
    }
}
class StudentComparer : IComparer<Student>
{
    public int Compare(Student x, Student y)
    {
        int marksCompare = y.Marks.CompareTo(x.Marks);
        if (marksCompare != 0)
            return marksCompare;

        return x.Age.CompareTo(y.Age);
    }
}