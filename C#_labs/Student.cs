using Exams;
using System.Text;

class Student
{
    private Person _person;
    private Education _education;
    private int _groupNumber;
    private Exam[] _exams;

    public Student(Person person, Education education, int groupNumber)
    {
        PersonInfo = person;
        EducationInfo = education;
        GroupInfo = groupNumber;
        Exams = Array.Empty<Exam>();
    }

    public Student() : this(new Person(), Education.Bachelor, 301)
    {
    }

    public Person PersonInfo
    {
        get => _person; init => _person = value ?? throw
            new ArgumentNullException(nameof(value));
    }
    public Education EducationInfo { get => _education; init => _education = value; }
    public int GroupInfo { get => _groupNumber; init => _groupNumber = value; }
    public Exam[] Exams { get => _exams; init => _exams = value ?? Array.Empty<Exam>(); }

    public double AverageGrade
    {
        get
        {
            if (Exams == null || Exams.Length == 0)
                return 0;

            int sum = 0;
            foreach (var exam in Exams)
            {
                sum += exam.Grade;
            }
            return (double)sum / Exams.Length;
        }
    }

    public bool this[Education eduIndex] => _education == eduIndex;

    public void AddExams(params Exam[] newExams)
    {
        if (newExams == null || newExams.Length == 0)
            return;

        int currentLength = Exams?.Length ?? 0;
        Exam[] newArray = new Exam[currentLength + newExams.Length];

        if (Exams != null)
            Array.Copy(Exams, newArray, currentLength);

        Array.Copy(newExams, 0, newArray, currentLength, newExams.Length);
        _exams = newArray;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder().AppendLine(
            $"Student Info:\n" +
            $"Person: {PersonInfo}\n" +
            $"Education: {EducationInfo}\n" +
            $"Group Number: {GroupInfo}\n" +
            $"Exams:");

        if (Exams == null || Exams.Length == 0)
        {
            sb.AppendLine("\n  No exams");
        }
        else
        {
            foreach (var exam in Exams)
            {
                sb.AppendLine($"\n  {exam}");
            }
        }

        return sb.ToString();
    }

    public virtual string ToShortString()
    {
        return $"Person: {PersonInfo}, Education: {EducationInfo}, Group: {GroupInfo}, Average Grade: {AverageGrade}";
    }
}