using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exams;

public delegate void StudentListHandler(object source, StudentListHandlerEventArgs args);

public class StudentCollection
{
    private List<Student> _students;
    private readonly Person _personComparer = new Person();
    private readonly StudentComparer _studentComparer = new StudentComparer();

    public event StudentListHandler StudentCountChanged;
    public event StudentListHandler StudentReferenceChanged;

    public string CollectionName { get; init; }

    public StudentCollection(string name = "DefaultCollection")
    {
        _students = new List<Student>();
        CollectionName = name;
    }

    public void AddDefaults()
    {
        var student1 = new Student(new Person("Іван", "Мельник", new DateTime(2000, 5, 15)), Education.Bachelor, 301);
        var student2 = new Student(new Person("Марія", "Іванова", new DateTime(1999, 8, 20)), Education.Master, 501);
        var student3 = new Student(new Person("Петро", "Щур", new DateTime(2001, 3, 10)), Education.Bachelor, 301);
        
        _students.Add(student1);
        OnStudentCountChanged(student1, "Додано студента за замовчуванням");
        
        _students.Add(student2);
        OnStudentCountChanged(student2, "Додано студента за замовчуванням");
        
        _students.Add(student3);
        OnStudentCountChanged(student3, "Додано студента за замовчуванням");
    }

    public void AddStudents(params Student[] students)
    {
        if (students is null || students.Length == 0)
            return;
            
        _students.AddRange(students);

        foreach (var student in students)
        {
            //_students.Add(student);
            OnStudentCountChanged(student, "Додано нового студента");
        }
    }
    
    // вид. студ. за індеком
    public bool Remove(int j)
    {
        if (j < 0 || j >= _students.Count)
            return false;
            
        Student removed = _students[j];
        _students.RemoveAt(j);
        OnStudentCountChanged(removed, "Видалено студента");
        return true;
    }
    
    // індексатор 
    public Student this[int index]
    {
        get 
        {
            if (index < 0 || index >= _students.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range");
            return _students[index];
        }
        set 
        {
            if (index < 0 || index >= _students.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range");
            
            _students[index] = value;
            OnStudentReferenceChanged(value, "Змінено посилання на студента");
        }
    }

    protected virtual void OnStudentCountChanged(Student student, string info)
    {
        StudentCountChanged?.Invoke(this, new StudentListHandlerEventArgs(CollectionName, info, student));
    }
    
    protected virtual void OnStudentReferenceChanged(Student student, string info)
    {
        StudentReferenceChanged?.Invoke(this, new StudentListHandlerEventArgs(CollectionName, info, student));
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Колекція студентів '{CollectionName}':");
        
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
        sb.AppendLine($"Колекція студентів '{CollectionName}' (скорочена):");
        
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