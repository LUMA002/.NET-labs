namespace Lab_7;

[Couple(Pair = "Student", Probability = 0.2, ChildType = "Girl")]
[Couple(Pair = "Botan", Probability = 0.5, ChildType = "Book")]
public sealed class SmartGirl : Human
{
    public SmartGirl(string name) : base(name, false) { }

    public string GetSmartName()
    {
        return Name;
    }
}