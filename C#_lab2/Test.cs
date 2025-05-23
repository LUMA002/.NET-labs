namespace Exams
{
    public class Test : IDateAndCopy
    {
        public string SubjectName { get; set; }
        public bool IsPassed { get; set; }
        public DateTime TestDate { get; set; }

        public Test(string subjectName, bool isPassed)
        {
            SubjectName = subjectName;
            IsPassed = isPassed;
            TestDate = DateTime.Now;
        }

        public Test() : this("Default Subject", false)
        {
        }

        // IDateAndCopy
        public DateTime Date
        {
            get => TestDate;
            init => TestDate = value;
        }

        public override string ToString() =>
            $"Subject: {SubjectName}, Passed: {(IsPassed ? "Yes" : "No")}, Date: {TestDate.ToShortDateString()}";

         object IDateAndCopy.DeepCopy()
        {
            Test copy = new Test(SubjectName, IsPassed);
            copy.TestDate = TestDate;
            return copy;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Test other = (Test)obj;
            return SubjectName == other.SubjectName &&
                   IsPassed == other.IsPassed &&
                   TestDate == other.TestDate;
        }

        public static bool operator ==(Test? left, Test? right)
        {
            if (ReferenceEquals(left, right))
                return true;
            if (left is null || right is null)
                return false;
            return left.Equals(right);
        }

        public static bool operator !=(Test? left, Test? right) => !(left == right);

        public override int GetHashCode() => HashCode.Combine(SubjectName, IsPassed, TestDate);
    }
}