namespace Lab_7;

[Couple(Pair = "Girl", Probability = 0.7, ChildType = "Girl")]
[Couple(Pair = "PrettyGirl", Probability = 0.4, ChildType = "PrettyGirl")]
[Couple(Pair = "SmartGirl", Probability = 0.5, ChildType = "Girl")]
public class Student : Human
{
    public Student(string name) : base(name, true) { }

    public string GetStudentName()
    {
        return Name;
    }
}
