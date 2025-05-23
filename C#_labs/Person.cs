class Person
{
    private string _name;
    private string _surname;
    private DateTime _dateOfBirth;

    public Person() : this(name: "Vasya", surname: "Pupkin", dateOfBirth: new DateTime(2000, 6, 12))
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

    public override string ToString()
    {
        return $"Name: {Name}, Surname: {Surname}, Date of birth: {DateOfBirth.ToShortDateString()}";
    }

    public virtual string ToShortString() => $"{Surname} {Name}";
}