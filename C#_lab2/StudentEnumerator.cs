using Exams;
using System.Collections;

public class StudentEnumerator : IEnumerator
{
    private readonly Student _student;
    private int _position = -1;
    private readonly ArrayList _commonSubjects;

    public StudentEnumerator(Student student)
    {
        _student = student;
        _commonSubjects = new ArrayList();

        // формуємо список назв предметів із tests
        ArrayList testSubjects = new ArrayList();

        foreach (Test test in _student.Tests)
            testSubjects.Add(test.SubjectName);
        // додаємо ті, що є і в exams, без дублювання
        foreach (Exam exam in _student.Exams)
        {
            string subj = exam.SubjectName;
            if (testSubjects.Contains(subj) && !_commonSubjects.Contains(subj))
                _commonSubjects.Add(subj);
        }
    }

    public bool MoveNext()
    {
        _position++;
        return _position < _commonSubjects.Count;
    }

    public void Reset() => _position = -1;

    public object Current
    {
        get
        {
            if (_position < 0 || _position >= _commonSubjects.Count)
                throw new InvalidOperationException();
            return _commonSubjects[_position] ?? throw new InvalidOperationException("Null reference encountered in _commonSubjects.");
        }
    }

}