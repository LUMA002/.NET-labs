using Exams;

public class StudentListHandlerEventArgs : EventArgs
{
    public string CollectionName { get; init; }
    public string ChangeInfo { get; init; }
    public Student Student { get; init; }

    public StudentListHandlerEventArgs(string collectionName, string changeInfo, Student student)
    {
        CollectionName = collectionName;
        ChangeInfo = changeInfo;
        Student = student;
    }

    public override string ToString()
    {
        return $"Колекція: {CollectionName}, Зміна: {ChangeInfo}, Студент: {Student?.ToShortString() ?? "null"}";
    }
}