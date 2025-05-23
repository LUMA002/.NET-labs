using Exams;

Console.OutputEncoding = System.Text.Encoding.UTF8;



Student originalStudent = new Student(
    new Person("Іван", "Мельник", new DateTime(2000, 5, 15)),
    Education.Bachelor,
    301
);

originalStudent.AddExams(
    new Exam("Програмування", 85, new DateTime(2025, 5, 10)),
    new Exam("Математика", 78, new DateTime(2025, 5, 20))
);

Student? copiedStudent = originalStudent.DeepCopy<Student>();
if (copiedStudent == null)
{
    Console.WriteLine("Помилка створення глибокої копії оригінального студента!");
    return;
}

// Перевірка, що це дійсно глибока копія
copiedStudent.PersonInfo = new Person("Петро", "Коваль", new DateTime(1999, 8, 12));
copiedStudent.Exams[0] = new Exam("Фізика", 92, new DateTime(2025, 5, 15));

Console.WriteLine("=== Оригінальний об'єкт: ===");
Console.WriteLine(originalStudent);

Console.WriteLine("=== Копія об'єкту: ===");
Console.WriteLine(copiedStudent);


Console.WriteLine("\n=== Робота з файлами ===");
Console.Write("Введіть ім'я файлу для збереження/завантаження даних: ");
string? filename = Console.ReadLine();

if (string.IsNullOrWhiteSpace(filename))
{
    filename = "student_data.json"; // за замовчуванням
    Console.WriteLine($"Використання імені файлу за замовчуванням: {filename}");
}

if (!File.Exists(filename))
{
    Console.WriteLine($"Файл '{filename}' не знайдено. Cтворюю новий файл...");
    originalStudent.Save(filename);
    Console.WriteLine("Файл успішно створено та збережено дані.");
}
else
{
    Console.WriteLine($"Файл '{filename}' існує. Завантажую дані...");
    if (originalStudent.Load(filename))
    {
        Console.WriteLine("Дані успішно завантажено.");
    }
    else
    {
        Console.WriteLine("Помилка при завантаженні даних.");
    }
}

Console.WriteLine("\n=== Поточний стан об'єкту: ===");
Console.WriteLine(originalStudent);


Console.WriteLine("\n=== Додавання нового іспиту ===");
if (originalStudent.AddFromConsole())
{
    Console.WriteLine("Збереження змін у файлі...");
    if (originalStudent.Save(filename))
    {
        Console.WriteLine("Зміни успішно збережено.");
    }
    else
    {
        Console.WriteLine("Помилка при збереженні змін.");
    }
}

Console.WriteLine("\n=== Оновлений стан об'єкту: ===");
Console.WriteLine(originalStudent);


Console.WriteLine("\n=== Використання статичних методів ===");

if (Student.Load(filename, originalStudent))
{
    Console.WriteLine("Дані успішно завантажено за допомогою статичного методу.");
}
else
{
    Console.WriteLine("Помилка при завантаженні даних за допомогою статичного методу.");
}

Console.WriteLine("\n=== Додавання нового іспиту (static) ===");
if (originalStudent.AddFromConsole())
{
    Console.WriteLine("Збереження змін у файлі за допомогою статичного методу...");
    if (Student.Save(filename, originalStudent))
    {
        Console.WriteLine("Зміни успішно збережено за допомогою статичного методу.");
    }
    else
    {
        Console.WriteLine("Помилка при збереженні змін за допомогою статичного методу.");
    }
}

Console.WriteLine("\n=== Фінальний стан об'єкту: ===");
Console.WriteLine(originalStudent);

