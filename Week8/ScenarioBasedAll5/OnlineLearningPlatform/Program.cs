using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineLearningPlatform
{
    public class DuplicateEnrollmentException : Exception
    {
        public DuplicateEnrollmentException(string message) : base(message) { }
    }

    public class CourseCapacityExceededException : Exception
    {
        public CourseCapacityExceededException(string message) : base(message) { }
    }

    public class AssignmentDeadlineException : Exception
    {
        public AssignmentDeadlineException(string message) : base(message) { }
    }

    public interface IRepository<T>
    {
        void Add(T item);
        IEnumerable<T> GetAll();
    }

    public class Repository<T> : IRepository<T>
    {
        private List<T> _data = new List<T>();

        public void Add(T item)
        {
            _data.Add(item);
        }

        public IEnumerable<T> GetAll()
        {
            return _data;
        }
    }

    public class Course : IComparable<Course>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int MaxCapacity { get; set; }
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public List<int> Ratings { get; set; } = new List<int>();

        public double AverageRating()
        {
            return Ratings.Count == 0 ? 0 : Ratings.Average();
        }

        public int CompareTo(Course other)
        {
            return this.Title.CompareTo(other.Title);
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Enrollment
    {
        public Student Student { get; set; }
        public Course Course { get; set; }
    }

    public class Assignment
    {
        public int Id { get; set; }
        public Course Course { get; set; }
        public DateTime Deadline { get; set; }

        public void Submit(DateTime submissionDate)
        {
            if (submissionDate > Deadline)
                throw new AssignmentDeadlineException("Submission after deadline.");
        }
    }

    class Program
    {
        static Repository<Course> courseRepo = new Repository<Course>();
        static Repository<Student> studentRepo = new Repository<Student>();
        static Repository<Instructor> instructorRepo = new Repository<Instructor>();
        static List<Enrollment> enrollments = new List<Enrollment>();

        static void Main(string[] args)
        {
            SeedData();

            EnrollStudent(1, 1);
            EnrollStudent(2, 1);
            EnrollStudent(1, 2);

            RunLinqReports();
        }

        static void SeedData()
        {
            courseRepo.Add(new Course { Id = 1, Title = "C# Advanced", MaxCapacity = 100 });
            courseRepo.Add(new Course { Id = 2, Title = "LINQ Mastery", MaxCapacity = 50 });

            studentRepo.Add(new Student { Id = 1, Name = "Rahul" });
            studentRepo.Add(new Student { Id = 2, Name = "Ritika" });

            instructorRepo.Add(new Instructor { Id = 1, Name = "Mr. Sharma" });

            var course1 = courseRepo.GetAll().First(c => c.Id == 1);
            course1.Ratings.AddRange(new[] { 5, 4, 5, 3 });

            var course2 = courseRepo.GetAll().First(c => c.Id == 2);
            course2.Ratings.AddRange(new[] { 4, 4, 5 });
        }

        static void EnrollStudent(int studentId, int courseId)
        {
            var student = studentRepo.GetAll().First(s => s.Id == studentId);
            var course = courseRepo.GetAll().First(c => c.Id == courseId);

            if (course.Enrollments.Any(e => e.Student.Id == studentId))
                throw new DuplicateEnrollmentException("Student already enrolled.");

            if (course.Enrollments.Count >= course.MaxCapacity)
                throw new CourseCapacityExceededException("Course capacity exceeded.");

            Enrollment enrollment = new Enrollment
            {
                Student = student,
                Course = course
            };

            enrollments.Add(enrollment);
            course.Enrollments.Add(enrollment);
        }

        static void RunLinqReports()
        {
            Console.WriteLine("\nCourses with more than 1 student:");
            var popularCourses = courseRepo.GetAll()
                .Where(c => c.Enrollments.Count > 1);

            foreach (var c in popularCourses)
                Console.WriteLine(c.Title);

            Console.WriteLine("\nStudents enrolled in more than 1 course:");
            var activeStudents = enrollments
                .GroupBy(e => e.Student)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key.Name);

            foreach (var s in activeStudents)
                Console.WriteLine(s);

            Console.WriteLine("\nMost Popular Course:");
            var mostPopular = courseRepo.GetAll()
                .OrderByDescending(c => c.Enrollments.Count)
                .FirstOrDefault();

            if (mostPopular != null)
                Console.WriteLine(mostPopular.Title);

            Console.WriteLine("\nAverage Course Ratings:");
            var ratings = courseRepo.GetAll()
                .Select(c => new
                {
                    Course = c.Title,
                    AvgRating = c.AverageRating()
                });

            foreach (var r in ratings)
                Console.WriteLine(r.Course + " - " + r.AvgRating);

            Console.WriteLine("\nInstructors with highest enrollments (Join):");
            var instructorStats =
                from i in instructorRepo.GetAll()
                join c in courseRepo.GetAll() on i.Id equals 1
                select new
                {
                    Instructor = i.Name,
                    TotalEnrollments = c.Enrollments.Count
                };

            foreach (var i in instructorStats)
                Console.WriteLine(i.Instructor + " - " + i.TotalEnrollments);

            Console.WriteLine("\nCustom Sorting (Courses by Title):");
            var sortedCourses = courseRepo.GetAll().ToList();
            sortedCourses.Sort();

            foreach (var c in sortedCourses)
                Console.WriteLine(c.Title);
        }
    }
}
