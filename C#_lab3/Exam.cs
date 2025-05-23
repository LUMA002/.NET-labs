namespace Exams
{
    public enum Education : byte
    {
        Master,
        Bachelor,
        SecondEducation
    }

    public class Exam : IDateAndCopy
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

        //IDateAndCopy
        public DateTime Date
        {
            get => ExamDate;
            init => ExamDate = value;
        }

        public object DeepCopy() => new Exam(SubjectName, Grade, ExamDate);

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Exam other = (Exam)obj;
            return SubjectName == other.SubjectName &&
                   Grade == other.Grade &&
                   ExamDate == other.ExamDate;
        }

        public static bool operator ==(Exam? left, Exam? right)
        {
            if (ReferenceEquals(left, right))
                return true;
            if (left is null || right is null)
                return false;
            return left.Equals(right);
        }

        public static bool operator !=(Exam? left, Exam? right) => !(left == right);

        public override int GetHashCode() => HashCode.Combine(SubjectName, Grade, ExamDate);

        public override string ToString() =>
            $"Subject: {SubjectName}, Grade: {Grade}, Exam Date: {ExamDate.ToShortDateString()}";
    }
} 