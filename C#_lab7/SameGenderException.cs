namespace Lab_7;

public class SameGenderException : Exception
{
    public SameGenderException() : base("Зустрілися дві людини однакової статі")
    {
    }
    public SameGenderException(string message) : base(message) { }
}
