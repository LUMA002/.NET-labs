using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exams;

public class StudentCollection
{
    private List<Student> _students;
    private readonly Person _personComparer = new Person();
    private readonly StudentComparer _studentComparer = new StudentComparer();

    public StudentCollection()
    {
        _students = new List<Student>();
    }

    public void AddDefaults()
    {
        _students.Add(new Student(new Person("Іван", "Мельник", new DateTime(2000, 5, 15)), Education.Bachelor, 301));
        _students.Add(new Student(new Person("Марія", "Іванова", new DateTime(1999, 8, 20)), Education.Master, 501));
        _students.Add(new Student(new Person("Петро", "Щур", new DateTime(2001, 3, 10)), Education.Bachelor, 301));
    }

    public void AddStudents(params Student[] students)
    {
        if (students is null || students.Length == 0)
            return;
        _students.AddRange(students);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Колекція студентів:");
        
        if (_students.Count == 0)
        {
            sb.AppendLine("  Колекція порожня");
            return sb.ToString();
        }

        foreach (Student student in _students)
        {
            sb.AppendLine("----------------------------------------------");
            sb.Append(student.ToString());
        }
        
        return sb.ToString();
    }

    public string ToShortString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Колекція студентів (скорочена):");
        
        if (_students.Count == 0)
        {
            sb.AppendLine("  Колекція порожня");
            return sb.ToString();
        }

        foreach (Student student in _students)
        {
            sb.AppendLine("----------------------------------------------");
            sb.AppendLine(student.ToShortString());
            sb.AppendLine($"Кількість заліків: {student.Tests.Count}");
            sb.AppendLine($"Кількість іспитів: {student.Exams.Count}");
        }
        
        return sb.ToString();
    }

    // сортування за прізвищем, IComparable
    public void SortBySurname()
    {
        _students.Sort();
    }

    // сортування за датою народження IComparer<Person>
    public void SortByDateOfBirth()
    {
        _students.Sort(_personComparer.Compare);
    }

    // сортування за авг балом IComparer<Student>
    public void SortByAverageGrade()
    {
        _students.Sort(_studentComparer);
    }

    //отримання максимального авг балу з вик. LINQ
    public double MaxAverageGrade
    {
        get => _students.Count > 0 ? _students.Max(s => s.AverageGrade) : 0;
    }

    //  отримання студентів з формою навчання Master 
    public IEnumerable<Student> MasterStudents
    {
        get => _students.Where(s => s.EducationInfo == Education.Master);
    }

    // групування студентів за авг балом
    public List<Student> AverageMarkGroup(double value)
    {
        return _students
            .Where(s => Math.Abs(s.AverageGrade - value) < 0.001)
            .ToList();
    }
}