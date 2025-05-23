namespace Exams;

enum Education
{
    Master,
    Bachelor,
    SecondEducation
}

class Exam
{
    public string SubjectName { get; set; }
    public int Grade { get; set; }
    public DateTime ExamDate { get; set; }

    public Exam(string subjectName, int grade, DateTime examDate)
    {
        SubjectName = subjectName;

        if (grade < 0 || grade > 100)
            throw new ArgumentOutOfRangeException(nameof(grade), "Grade must be between 0 and 100");

        Grade = grade;
        ExamDate = examDate;
    }

    public Exam() : this("Math", 55, new DateTime(2025, 11, 23))
    {
    }

    public override string ToString()
    {
        return $"Subject: {SubjectName}, Grade: {Grade}, Exam Date: {ExamDate.ToShortDateString()}";
    }
}