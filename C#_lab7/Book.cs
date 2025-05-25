namespace Lab_7;

public class Book : IHasName
{
    public string Name { get; set; } = string.Empty;

    public Book(string name)
    {
        Name = "Booooooooooook";
    }
}