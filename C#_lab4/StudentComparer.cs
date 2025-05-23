using System.Collections.Generic;

public class StudentComparer : IComparer<Student>
{
    public int Compare(Student? x, Student? y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        
        return x.AverageGrade.CompareTo(y.AverageGrade);
    }
} 