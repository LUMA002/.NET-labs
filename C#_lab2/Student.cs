using System.Collections;
using System.Text;
using Exams;

public class Student : Person, IDateAndCopy
{
    private Education _education;
    private int _groupNumber;
    private ArrayList _tests;
    private ArrayList _exams;


    public Student(Person person, Education education, int groupNumber)
        : base(person.Name, person.Surname, person.DateOfBirth)
    {
        EducationInfo = education;
        GroupInfo = groupNumber; 
        Tests = new ArrayList();
        Exams = new ArrayList();
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

    public ArrayList Exams
    {
        get => _exams = _exams ?? new ArrayList();
        init => _exams = value;
    }

    public ArrayList Tests
    {
        get => _tests = _tests ?? new ArrayList();
        init => _tests = value;
    }


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
        sb.AppendLine($"Student Info:\n" +
           $"Person: {base.ToString()}\n" +
           $"Education: {EducationInfo}\n" +
           $"Group Number: {GroupInfo}\n\n" );

        sb.AppendLine("Tests:");
        if (Tests.Count == 0)
            sb.AppendLine("  No tests");
        else
        {
            foreach (Test test in Tests)
                sb.AppendLine($"  {test}");
        }

        sb.AppendLine("Exams:");
        if (Exams.Count == 0)
            sb.AppendLine("  No exams");
        else
        {
            foreach (Exam exam in Exams)
                sb.AppendLine($"  {exam}");
        }

        return sb.ToString();
    }

    public override string ToShortString()
    {
        return $"Person: {base.ToString()}, Education: {EducationInfo}, Group: {GroupInfo}, Average Grade: {AverageGrade:F2}";
    }

    public override object DeepCopy()
    {
        Student copy = new Student(
            (Person)base.DeepCopy(),
            EducationInfo,
            GroupInfo
        );

        // копіювання тестів
        foreach (IDateAndCopy test in Tests)
            copy.Tests.Add(test.DeepCopy());
        //  копіювання іспитів
        foreach (Exam exam in Exams)
            copy.Exams.Add(exam.DeepCopy());
        return copy;
    }

    // ітератор об'єднання всіх елементів з tests та exams
    public IEnumerable GetTestsAndExams()
    {
        foreach (var test in Tests)
            yield return test;
        foreach (var exam in Exams)
            yield return exam;
    }

    public IEnumerable GetExamsWithHigherGrade(int minGrade)
    {
        foreach (Exam exam in Exams)
            if (exam.Grade > minGrade)
                yield return exam;
    }






    // IEnumerator – перебір назв предметів, котрі є в обох колекціях

    public IEnumerator GetEnumerator() => new StudentEnumerator(this);

    public IEnumerable GetPassedTestsAndExams()
    {
        foreach (Exam exam in Exams)
            if (exam.Grade > 2)
                yield return exam;
        foreach (Test test in Tests)
            if (test.IsPassed)
                yield return test;
    }


    public IEnumerable GetPassedTestsWithPassedExams()
    {
        ArrayList passedExamSubjects = new ArrayList();
        foreach (Exam exam in Exams)
            if (exam.Grade > 2 && !passedExamSubjects.Contains(exam.SubjectName))
                passedExamSubjects.Add(exam.SubjectName);
        foreach (Test test in Tests)
            if (test.IsPassed && passedExamSubjects.Contains(test.SubjectName))
                yield return test;
    }
}