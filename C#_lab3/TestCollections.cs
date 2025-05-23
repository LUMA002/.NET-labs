using System.Collections.Generic;
using System.Diagnostics;
using Exams;

public class TestCollections
{
    private List<Person> _listPerson;
    private List<string> _listString;
    private Dictionary<Person, Student> _dictPersonStudent;
    private Dictionary<string, Student> _dictStringStudent;
    private int _size;

    public TestCollections(int size)
    {
        _size = size;
        _listPerson = new List<Person>(size);
        _listString = new List<string>(size);
        _dictPersonStudent = new Dictionary<Person, Student>(size);
        _dictStringStudent = new Dictionary<string, Student>(size);

        for (int i = 0; i < size; i++)
        {
            Student student = GenerateElement(i);
            Person person = student.GetPersonFromStudent;
            string personString = person.ToString();

            _listPerson.Add(person);
            _listString.Add(personString);
            _dictPersonStudent.Add(person, student);
            _dictStringStudent.Add(personString, student);
        }
    }

    // генерація елементів Student з "взаємно-однозначною відповідністю числовому параметру"
    public static Student GenerateElement(int n)
    {
        // детермінований об'єкт Person на основі параметра n
        string name = $"Ім'я{n}";
        string surname = $"Прізвище{n}";
        DateTime dateOfBirth = new DateTime(2000 + (n % 20), 1 + (n % 12), 1 + (n % 28));
        
        Person person = new Person(name, surname, dateOfBirth);
        
        // Student з використанням Person
        Education education = (Education)(n % 3);
        int groupNumber = 101 + (n % 597);
        
        Student student = new Student(person, education, groupNumber);
        

        student.AddExams(
            new Exam($"Предмет{n % 5}", 60 + (n % 40), dateOfBirth.AddDays(n % 30)),
            new Exam($"Предмет{(n + 2) % 5}", 60 + ((n + 10) % 40), dateOfBirth.AddDays((n + 5) % 30))
        );
        
        student.AddTests(
            new Test($"Предмет{n % 5}", n % 2 == 0),
            new Test($"Предмет{(n + 2) % 5}", n % 3 == 0)
        );
        
        return student;
    }

    public void MeasureSearchTime()
    {
        Console.WriteLine("Вимірювання часу пошуку в колекціях:");
        
        // елементи для тестування

        Student firstStudent = GenerateElement(0);
        Person firstPerson = firstStudent;
        string firstString = firstPerson.ToString();
        ;
        
        int middle = _size / 2;

        Student middleStudent = GenerateElement(middle);
        Person middlePerson = middleStudent;
        string middleString = middlePerson.ToString();

        int last = _size - 1;
   
        Student lastStudent = GenerateElement(last);
        Person lastPerson = lastStudent;
        string lastString = lastPerson.ToString();

        // Елемент, відсутній у колекціях
        Student notInCollectionStudent = GenerateElement(_size);
        Person notInCollectionPerson = notInCollectionStudent.GetPersonFromStudent;
        string notInCollectionString = notInCollectionPerson.ToString();

        // масиви елементів для пошуку
        Person[] personsToSearch = { firstPerson, middlePerson, lastPerson, notInCollectionPerson };
        string[] stringsToSearch = { firstString, middleString, lastString, notInCollectionString };
        Student[] studentsToSearch = { firstStudent, middleStudent, lastStudent, notInCollectionStudent };
        string[] elementPosition = { "Перший", "Центральний", "Останній", "Відсутній" };
        
        Stopwatch stopwatch = new Stopwatch();
        
        for (int i = 0; i < 4; i++)
        {
            Console.WriteLine($"\n--- Елемент: {elementPosition[i]} ---");
            
            // пошук в List<Person>
            stopwatch.Restart();
            bool containsPerson = _listPerson.Contains(personsToSearch[i]);
            stopwatch.Stop();
            Console.WriteLine($"List<Person>.Contains: {stopwatch.ElapsedTicks} тіків, Результат: {containsPerson}");
            

            stopwatch.Restart();
            bool containsString = _listString.Contains(stringsToSearch[i]);
            stopwatch.Stop();
            Console.WriteLine($"List<string>.Contains: {stopwatch.ElapsedTicks} тіків, Результат: {containsString}");
            
            // пошук за ключем у Dictionary<Person, Student>
            stopwatch.Restart();
            bool containsPersonKey = _dictPersonStudent.ContainsKey(personsToSearch[i]);
            stopwatch.Stop();
            Console.WriteLine($"Dictionary<Person, Student>.ContainsKey: {stopwatch.ElapsedTicks} тіків, Результат: {containsPersonKey}");
            
            // пошук за ключем у Dictionary<string, Student>
            stopwatch.Restart();
            bool containsStringKey = _dictStringStudent.ContainsKey(stringsToSearch[i]);
            stopwatch.Stop();
            Console.WriteLine($"Dictionary<string, Student>.ContainsKey: {stopwatch.ElapsedTicks} тіків, Результат: {containsStringKey}");
            
            // пошук за значенням у Dictionary<Person, Student>
            stopwatch.Restart();
            bool containsStudentValue = _dictPersonStudent.ContainsValue(studentsToSearch[i]);
            stopwatch.Stop();
            Console.WriteLine($"Dictionary<Person, Student>.ContainsValue: {stopwatch.ElapsedTicks} тіків, Результат: {containsStudentValue}");
        }
    }
} 