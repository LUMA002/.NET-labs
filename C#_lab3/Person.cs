public class Person : IDateAndCopy, IComparable, IComparer<Person>
{
    protected string _name;
    protected string _surname;
    protected DateTime _dateOfBirth;

    public Person() : this("Vasya", "Pupkin", new DateTime(2000, 6, 12))
    {
    }

    public Person(string name, string surname, DateTime dateOfBirth)
    {
        Name = name;
        Surname = surname;
        DateOfBirth = dateOfBirth;
    }

    public string Name
    {
        get => _name;
        init => _name = value;
    }

    public string Surname
    {
        get => _surname;
        init => _surname = value;
    }

    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set => _dateOfBirth = value;
    }

    public int YearOfBirthday
    {
        get => DateOfBirth.Year;
        set => DateOfBirth = new DateTime(value, DateOfBirth.Month, DateOfBirth.Day);
    }

    // DateAndCopy interface implementation
    public DateTime Date
    {
        get => DateOfBirth;
        init => DateOfBirth = value;
    }

    public virtual object DeepCopy() => new Person(Name, Surname, DateOfBirth);

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
        Person other = (Person)obj;
        return Name == other.Name &&
               Surname == other.Surname &&
               DateOfBirth == other.DateOfBirth;
    }

    public static bool operator ==(Person? left, Person? right)
    {
        if (ReferenceEquals(left, right))
            return true;
        if (left is null || right is null)
            return false;
        return left.Equals(right);
    }

    public static bool operator !=(Person? left, Person? right) => !(left == right);

    public override int GetHashCode() => HashCode.Combine(Name, Surname, DateOfBirth);

    public override string ToString() =>
        $"Name: {Name}, Surname: {Surname}, Date of birth: {DateOfBirth.ToShortDateString()}";

    public virtual string ToShortString() => $"{Surname} {Name}";

    // IComparable
    public int CompareTo(object? obj)
    {
        if (obj is Person person)
            return String.Compare(Surname, person.Surname, StringComparison.Ordinal);
        throw new ArgumentException("Object is not a Person");
    }

    // IComparable
    public int Compare(Person? x, Person? y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        
        return DateTime.Compare(x.DateOfBirth, y.DateOfBirth);
    }
} 