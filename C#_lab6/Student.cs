using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Exams;

[Serializable] // необов'язковий для Text.Json
public class Student : Person, IDateAndCopy
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        WriteIndented = true,
        ReferenceHandler = ReferenceHandler.Preserve
    };
    
    private Education _education;
    private int _groupNumber;
    private List<Test> _tests;
    private List<Exam> _exams;

    public Student(Person person, Education education, int groupNumber)
        : base(person.Name, person.Surname, person.DateOfBirth)
    {
        EducationInfo = education;
        GroupInfo = groupNumber;
        Tests = new List<Test>();
        Exams = new List<Exam>();
    }

    public Student() : this(new Person("Default", "Person", new DateTime(2000, 1, 1)), Education.Bachelor, 301)
    { }

    public new Person PersonInfo
    {
        get => this;
        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            Name = value.Name;
            Surname = value.Surname;
            DateOfBirth = value.DateOfBirth;
        }
    }

    public Education EducationInfo
    {
        get => _education;
        set => _education = value;
    }

    public int GroupInfo
    {
        get => _groupNumber;
        set
        {
            if (value <= 100 || value >= 699)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "Group number must be between 101 and 698");
            _groupNumber = value;
        }
    }

    public List<Exam> Exams
    {
        get => _exams = _exams ?? new List<Exam>();
        init => _exams = value;
    }

    public List<Test> Tests
    {
        get => _tests = _tests ?? new List<Test>();
        init => _tests = value;
    }

    public Person GetPersonFromStudent => new Person(Name, Surname, DateOfBirth);

    public double AverageGrade
    {
        get
        {
            if (Exams.Count == 0)
                return 0;
            double sum = 0;
            foreach (Exam exam in Exams)
            {
                sum += exam.Grade;
            }
            return sum / Exams.Count;
        }
    }

    public bool this[Education eduIndex] => EducationInfo == eduIndex;

    public void AddExams(params Exam[] newExams)
    {
        if (newExams is null || newExams.Length == 0)
            return;
        Exams.AddRange(newExams);
    }

    public void AddTests(params Test[] newTests)
    {
        if (newTests == null || newTests.Length == 0)
            return;
        Tests.AddRange(newTests);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Інформація про студента:\n" +
           $"Персона: {base.ToString()}\n" +
           $"Форма навчання: {EducationInfo}\n" +
           $"Номер групи: {GroupInfo}\n\n");

        sb.AppendLine("Заліки:");
        if (Tests.Count == 0)
            sb.AppendLine("  Немає заліків");
        else
        {
            foreach (Test test in Tests)
                sb.AppendLine($"  {test}");
        }

        sb.AppendLine("Іспити:");
        if (Exams.Count == 0)
            sb.AppendLine("  Немає іспитів");
        else
        {
            foreach (Exam exam in Exams)
                sb.AppendLine($"  {exam}");
        }

        return sb.ToString();
    }

    public override string ToShortString()
    {
        return $"Персона: {base.ToString()}, Форма навчання: {EducationInfo}, Група: {GroupInfo}, Середній бал: {AverageGrade:F2}";
    }
    
    public override object DeepCopy()
    {
        return DeepCopy<Student>();
    }
    
// DeepCopy з використанням серіалізації через MemoryStream
public T? DeepCopy<T>() where T : class
{
    try
    {
        using (MemoryStream stream = new MemoryStream())
        {
            JsonSerializer.Serialize(stream, this, JsonSerializerOptions);
            // курсор на початок потоку для читання
            stream.Position = 0;
            return JsonSerializer.Deserialize<T>(stream, JsonSerializerOptions);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Помилка при глибокому копіюванні: {ex.Message}");
        return null;
    }
}

// збереження даних об'єкту в файлі за допомогою серіалізації
   public bool Save(string filename)
{
    if (string.IsNullOrEmpty(filename)) return false;
    
    FileStream? fileStream = null;
    try
    {
        fileStream = new FileStream(filename, FileMode.Create);
        JsonSerializer.Serialize(fileStream, this, JsonSerializerOptions);
        return true;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Помилка при збереженні файлу: {ex.Message}");
        return false;
    }
    finally
    {
        fileStream?.Dispose();
    }
}

// ініціалізації об'єкту даними з файлу за допомогою десеріалізації
public bool Load(string filename)
{
    if (string.IsNullOrEmpty(filename)) return false;
    if (!File.Exists(filename))
    {
        Console.WriteLine($"Файл {filename} не існує.");
        return false;
    }

    FileStream? fileStream = null;
    try
    {
        fileStream = new FileStream(filename, FileMode.Open);
        Student? tempStudent = JsonSerializer.Deserialize<Student>(fileStream, JsonSerializerOptions);

        if (tempStudent != null)
        {
            PersonInfo = tempStudent.PersonInfo;
            EducationInfo = tempStudent.EducationInfo;
            GroupInfo = tempStudent.GroupInfo;
            
            Exams.Clear();
            Exams.AddRange(tempStudent.Exams);
            
            Tests.Clear();
            Tests.AddRange(tempStudent.Tests);
            return true;
        }
        return false;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Помилка при завантаженні файлу: {ex.Message}");
        return false;
    }
    finally
    {
        fileStream?.Dispose();
    }
}
    // збереження об'єкту в файлі за допомогою серіалізації
    public static bool Save<T>(string filename, T obj) where T : Student
    {
        return obj != null && obj.Save(filename);
    }

    public static bool Load<T>(string filename, T obj) where T : Student
    {
        return obj != null && obj.Load(filename);
    }
    
    // додавання нового іспиту з даними з консолі
    public bool AddFromConsole()
    {
        Console.WriteLine("Додавання нового іспиту");
        Console.WriteLine("Введіть дані у форматі: [назва предмету];[оцінка (0-100)];[дата іспиту (дд.мм.рррр)]");
        Console.WriteLine("Приклад: Математика;85;20.05.2025");

        string? input = Console.ReadLine();

        try
        {
            string[] parts = input?.Split(';') ?? Array.Empty<string>();
            if (parts.Length != 3)
            {
                throw new FormatException("Неправильний формат введених даних. Потрібно ввести 3 значення, розділені ';'.");
            }

            string subjectName = parts[0].Trim();
            if (string.IsNullOrWhiteSpace(subjectName))
            {
                throw new ArgumentException("Назва предмету не може бути порожньою.");
            }

            if (!int.TryParse(parts[1].Trim(), out int grade))
            {
                throw new FormatException("Неправильний формат оцінки. Має бути ціле число від 0 до 100.");
            }

            if (grade < 0 || grade > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(grade), "Оцінка повинна бути в межах від 0 до 100.");
            }

            if (!DateTime.TryParse(parts[2].Trim(), out DateTime examDate))
            {
                throw new FormatException("Неправильний формат дати. Використовуйте формат дд.мм.рррр.");
            }

            Exam newExam = new Exam(subjectName, grade, examDate);
            Exams.Add(newExam);
            Console.WriteLine($"Іспит додано: {newExam}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка при додаванні іспиту: {ex.Message}");
            return false;
        }
    }

    // ітератор об'єднання всіх елементів з tests та exams
    public IEnumerable<object> GetTestsAndExams()
    {
        foreach (var test in Tests)
            yield return test;
        foreach (var exam in Exams)
            yield return exam;
    }

    // отримати іспити з оцінкою вище зазначеної
    public IEnumerable<Exam> GetExamsWithHigherGrade(int minGrade)
    {
        foreach (Exam exam in Exams)
            if (exam.Grade > minGrade)
                yield return exam;
    }

    // успішно здані заліки та іспити
    public IEnumerable<object> GetPassedTestsAndExams()
    {
        foreach (Exam exam in Exams)
            if (exam.Grade > 2)
                yield return exam;
        foreach (Test test in Tests)
            if (test.IsPassed)
                yield return test;
    }

    //  успішно здані заліки та відповідні успішно здані іспити
    public IEnumerable<Test> GetPassedTestsWithPassedExams()
    {
        List<string> passedExamSubjects = new List<string>();
        foreach (Exam exam in Exams)
            if (exam.Grade > 2 && !passedExamSubjects.Contains(exam.SubjectName))
                passedExamSubjects.Add(exam.SubjectName);
        
        foreach (Test test in Tests)
            if (test.IsPassed && passedExamSubjects.Contains(test.SubjectName))
                yield return test;
    }
}