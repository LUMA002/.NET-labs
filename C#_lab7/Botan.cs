namespace Lab_7;

[Couple(Pair = "Girl", Probability = 0.7, ChildType = "SmartGirl")]
[Couple(Pair = "PrettyGirl", Probability = 0.1, ChildType = "PrettyGirl")]
[Couple(Pair = "SmartGirl", Probability = 0.8, ChildType = "Book")]
public class Botan : Human
{
    public Botan(string name) : base(name, true) { }

    public string GetBotanName()
    {
        return Name;
    }
}