using System.Collections.Generic;
using System.Text;

public class Journal
{
    private List<JournalEntry> _entries;

    public Journal()
    {
        _entries = new List<JournalEntry>();
    }

    private void LogEvent(object source, StudentListHandlerEventArgs args)
    {
        _entries.Add(new JournalEntry(
            args.CollectionName,
            args.ChangeInfo,
            args.Student?.ToShortString() ?? "null"
        ));
    }

    public void StudentCountChanged(object source, StudentListHandlerEventArgs args)
    {
        LogEvent(source, args);
    }

    public void StudentReferenceChanged(object source, StudentListHandlerEventArgs args)
    {
        LogEvent(source, args);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Журнал змін в колекціях:");
        
        if (_entries.Count == 0)
        {
            sb.AppendLine("  Журнал порожній");
            return sb.ToString();
        }

        for (int i = 0; i < _entries.Count; i++)
        {
            sb.AppendLine($"Запис #{i + 1}: {_entries[i]}");
        }
        
        return sb.ToString();
    }
}