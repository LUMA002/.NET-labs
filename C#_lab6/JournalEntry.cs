using Exams;

public class JournalEntry
{
    public string CollectionName { get; init; }
    public string ChangeInfo { get; init; }
    public string StudentInfo { get; init; }

    public JournalEntry(string collectionName, string changeInfo, string studentInfo)
    {
        CollectionName = collectionName;
        ChangeInfo = changeInfo;
        StudentInfo = studentInfo;
    }

    public override string ToString()
    {
        return $"Колекція: {CollectionName}, Зміна: {ChangeInfo}, Студент: {StudentInfo}";
    }
}