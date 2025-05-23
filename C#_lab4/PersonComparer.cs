
public class PersonComparer : IComparer<Person>
{
    public int Compare(Person? x, Person? y)
    {
        if (x == null && y == null)
            return 0;
        if (x == null)
            return -1;
        if (y == null)
            return 1;

        int surnameComparison = string.Compare(x.Surname, y.Surname);
        if (surnameComparison != 0)
            return surnameComparison;

        int nameComparison = string.Compare(x.Name, y.Name);
        if (nameComparison != 0)
            return nameComparison;

        return x.Date.CompareTo(y.Date);
    }
}