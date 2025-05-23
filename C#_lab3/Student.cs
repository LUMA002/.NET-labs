using System.Collections.Generic;
using System.Text;
using Exams;

public class Student : Person, IDateAndCopy
{
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
        init
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
        init => _education = value;
    }

    public int GroupInfo
    {
        get => _groupNumber;
        init
        {
            if (value <= 100 || value >= 699)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "Group number must be between 101 and 698");
            _groupNumber = value;
        }
    }

    // з масиву в список
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

    // об'єкт Person з ідентичними даними, що й базові, але посилається на новий об'єкт типу Person
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
        Student copy = new Student(
            (Person)base.DeepCopy(),
            EducationInfo,
            GroupInfo
        );

        //  копіювання заліків
        foreach (Test test in Tests)
            copy.Tests.Add((Test)test.DeepCopy());
        
        // копіювання іспитів
        foreach (Exam exam in Exams)
            copy.Exams.Add((Exam)exam.DeepCopy());
        
        return copy;
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