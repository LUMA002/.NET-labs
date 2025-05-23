using System;
using System.Collections.Generic;
using Exams;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // 1. Створення та тестування колекції StudentCollection
        Console.WriteLine("Створення колекції StudentCollection:");
        StudentCollection studentCollection = new StudentCollection();
        studentCollection.AddDefaults();

        // додавання додаткових студентів з різними оцінками, тестування групування
        Student student1 = new Student(new Person("Іван", "Петров", new DateTime(1998, 5, 15)), Education.Bachelor, 301);
        student1.AddExams(
            new Exam("Програмування", 85, new DateTime(2025, 5, 15)),
            new Exam("Математика", 85, new DateTime(2025, 5, 20))
        );
        student1.AddTests(
            new Test("Програмування", true),
            new Test("Англійська", false)
        );

        Student student2 = new Student(new Person("Олена", "Сидоренко", new DateTime(1997, 8, 10)), Education.Master, 401);
        student2.AddExams(
            new Exam("Бази даних", 90, new DateTime(2025, 6, 5)),
            new Exam("Мережі", 85, new DateTime(2025, 6, 10))
        );
        student2.AddTests(
            new Test("Бази даних", true),
            new Test("Операційні системи", true)
        );

        studentCollection.AddStudents(student1, student2);

        Console.WriteLine(studentCollection);
        
        // 2. Тестування методів сортування
        Console.WriteLine("\n----- Сортування за прізвищем -----");
        studentCollection.SortBySurname();
        Console.WriteLine(studentCollection.ToShortString());
        
        Console.WriteLine("\n----- Сортування за датою народження -----");
        studentCollection.SortByDateOfBirth();
        Console.WriteLine(studentCollection.ToShortString());
        
        Console.WriteLine("\n----- Сортування за середнім балом -----");
        studentCollection.SortByAverageGrade();
        Console.WriteLine(studentCollection.ToShortString());
        
        // 3. Тестування LINQ операцій
        Console.WriteLine("\n----- LINQ операції -----");
        Console.WriteLine($"Максимальний середній бал: {studentCollection.MaxAverageGrade:F2}");
        
        Console.WriteLine("\nСтуденти з формою навчання Master:");
        foreach (var student in studentCollection.MasterStudents)
        {
            Console.WriteLine($"  {student.ToShortString()}");
        }
        
        // групування за середнім балом
        double targetGrade = 85.0;
        Console.WriteLine($"\nСтуденти з середнім балом = {targetGrade:F2}:");
        List<Student> studentsWithGrade = studentCollection.AverageMarkGroup(targetGrade);
        foreach (var student in studentsWithGrade)
        {
            Console.WriteLine($"  {student.ToShortString()}");
        }

        // 4. Тестування TestCollections
        Console.WriteLine("\n----- Тестування продуктивності колекцій -----");
        int collectionSize = GetValidCollectionSize();
        
        Console.WriteLine($"\nСтворення колекцій з {collectionSize} елементами...");
        TestCollections testCollections = new TestCollections(collectionSize);
        testCollections.MeasureSearchTime();

        TestCollections.RunDemo(collectionSize);

        Console.WriteLine("\nТестування завершено.");

        Console.WriteLine("\nНатисніть будь-яку клавішу для завершення...");
        Console.ReadKey();
    }

    private static int GetValidCollectionSize()
    {
        int size;
        bool isValid = false;
        
        do
        {
            Console.Write("Введіть розмір колекцій для тестування (додатне ціле число): ");
            string input = Console.ReadLine();

            
            isValid = int.TryParse(input, out size) && size > 0;
            
            if (!isValid)
            {
                Console.WriteLine("Помилка: введіть додатне ціле число.");
            }
            
        } while (!isValid);
        
        return size;
    }
}