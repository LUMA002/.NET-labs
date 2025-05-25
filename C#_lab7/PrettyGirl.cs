namespace Lab_7;

[Couple(Pair = "Student", Probability = 1.0, ChildType = "PrettyGirl")]
[Couple(Pair = "Botan", Probability = 0.1, ChildType = "PrettyGirl")]
public sealed class PrettyGirl : Human
{
    public PrettyGirl(string name) : base(name, false) { }

    public string GetPrettyName()
    {
        return Name;
    }
}