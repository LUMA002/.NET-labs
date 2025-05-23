using System;
using Exams;

class Program
{
    static void Main()
    {
        Console.WriteLine("----------------- Testing Person Class -----------------");
        Person person1 = new Person("Vanya", "Nelisiy", new DateTime(2000, 7, 18));
        Person person2 = new Person("Vanya", "Nelisiy", new DateTime(2000, 7, 18));

        Console.WriteLine($"person1: {person1}");
        Console.WriteLine($"person2: {person2}");
        Console.WriteLine($"References are equal: {ReferenceEquals(person1, person2)}");
        Console.WriteLine($"Objects are equal (Equals): {person1.Equals(person2)}");
        Console.WriteLine($"Using == operator: {person1 == person2}");
        Console.WriteLine($"HashCode person1: {person1.GetHashCode()}");
        Console.WriteLine($"HashCode person2: {person2.GetHashCode()}");

        Console.WriteLine("\n----------------- Testing Student Class -----------------");
        Student student = new Student(
        new Person("Olexandr", "Push", new DateTime(2000, 3, 10)), Education.Master, 502);

        student.AddExams(
        new Exam("Programming", 95, new DateTime(2025, 12, 15)),
        new Exam("Math", 90, new DateTime(2025, 12, 20)),
        new Exam("English", 75, new DateTime(2025, 12, 25)),
        new Exam("Physics", 60, new DateTime(2025, 12, 30))
        );

        student.AddTests(
        new Test("Programming", true),
        new Test("Math", true),
        new Test("History", false),
        new Test("Physics", true)
        );

        Console.WriteLine(student);

        Console.WriteLine("\nPerson property value:");
        Console.WriteLine(student.PersonInfo);

        Console.WriteLine("\n----------------- Testing DeepCopy -----------------");
        Student studentCopy = (Student)student.DeepCopy();
        student.DateOfBirth = new DateTime(2001, 4, 20);
        student.AddExams(new Exam("New Exam", 85, DateTime.Now));
        student.AddTests(new Test("New Test", true));

        Console.WriteLine("\nOriginal student after modification:");
        Console.WriteLine(student);
        Console.WriteLine("\nStudent copy (should be unchanged):");
        Console.WriteLine(studentCopy);

        Console.WriteLine("\n----------------- Testing Exception Handling -----------------");
        try
        {
            Student invalidStudent = new Student()
            {
                PersonInfo = new Person("Invalid", "Student", new DateTime(2000, 1, 1)),
                EducationInfo = Education.Bachelor,
                GroupInfo = 50
            };
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Exception caught: {ex.Message}");
        }

        Console.WriteLine("\n----------------- Testing Iterators -----------------");
        Console.WriteLine("\nExams with grade > 3:");
        foreach (Exam exam in student.GetExamsWithHigherGrade(3))
        {
            Console.WriteLine($"  {exam}");
        }

        Console.WriteLine("\n----------------- Additional Tasks -----------------");

        Console.WriteLine("\nSubjects that are in both tests and exams (intersection):");
        foreach (string subject in student)
        {
            Console.WriteLine($"  {subject}");
        }

        Console.WriteLine("\nPassed tests and exams:");
        foreach (object item in student.GetPassedTestsAndExams())
        {
            Console.WriteLine($"  {item}");
        }

        Console.WriteLine("\nPassed tests that also have a passed exam:");
        foreach (Test test in student.GetPassedTestsWithPassedExams())
        {
            Console.WriteLine($"  {test}");
        }

        Console.WriteLine("\nPassed tests that also have a passed exam (default GetEnumerator()):");
        foreach (Object test in student)
        {
            Console.WriteLine($"  {test}");
        }
    }
}