using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using Exams;
using System;

public class TestCollections
{
    private List<Person> _listPerson;
    private List<string> _listString;
    private Dictionary<Person, Student> _dictPersonStudent;
    private Dictionary<string, Student> _dictStringStudent;
    private int _size;

    private ImmutableList<Person> _immutableListPerson;
    private ImmutableList<string> _immutableListString;
    private ImmutableDictionary<Person, Student> _immutableDictPersonStudent;
    private ImmutableDictionary<string, Student> _immutableDictStringStudent;

    private SortedList<Person, Student> _sortedListPersonStudent;
    private SortedList<string, Student> _sortedListStringStudent;
    private SortedDictionary<Person, Student> _sortedDictPersonStudent;
    private SortedDictionary<string, Student> _sortedDictStringStudent;

    public TestCollections(int size)
    {
        _size = size;
        _listPerson = new List<Person>(size);
        _listString = new List<string>(size);
        _dictPersonStudent = new Dictionary<Person, Student>(size);
        _dictStringStudent = new Dictionary<string, Student>(size);

        var tempPersonList = new List<Person>(size);
        var tempStringList = new List<string>(size);
        var tempPersonStudentDict = new Dictionary<Person, Student>(size);
        var tempStringStudentDict = new Dictionary<string, Student>(size);


        _sortedListPersonStudent = new SortedList<Person, Student>(new PersonComparer());
        _sortedListStringStudent = new SortedList<string, Student>();
        _sortedDictPersonStudent = new SortedDictionary<Person, Student>(new PersonComparer());
        _sortedDictStringStudent = new SortedDictionary<string, Student>();


        for (int i = 0; i < size; i++)
        {
            Student student = GenerateElement(i);
            Person person = student.GetPersonFromStudent;
            string personString = person.ToString();

            _listPerson.Add(person);
            _listString.Add(personString);
            _dictPersonStudent.Add(person, student);
            _dictStringStudent.Add(personString, student);

            tempPersonList.Add(person);
            tempStringList.Add(personString);
            tempPersonStudentDict.Add(person, student);
            tempStringStudentDict.Add(personString, student);

            
            _sortedListPersonStudent.Add(person, student);
            _sortedListStringStudent.Add(personString, student);
            _sortedDictPersonStudent.Add(person, student);
            _sortedDictStringStudent.Add(personString, student);
        }

        _immutableListPerson = ImmutableList.CreateRange(tempPersonList);
        _immutableListString = ImmutableList.CreateRange(tempStringList);
        _immutableDictPersonStudent = ImmutableDictionary.CreateRange(tempPersonStudentDict);
        _immutableDictStringStudent = ImmutableDictionary.CreateRange(tempStringStudentDict);
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

        // елемент відсутній у колекціях
        Student notInCollectionStudent = GenerateElement(_size);
        Person notInCollectionPerson = notInCollectionStudent.GetPersonFromStudent;
        string notInCollectionString = notInCollectionPerson.ToString();

        // масиви елементів для пошуку
        Person[] personsToSearch = { firstPerson, middlePerson, lastPerson, notInCollectionPerson };
        string[] stringsToSearch = { firstString, middleString, lastString, notInCollectionString };
        Student[] studentsToSearch = { firstStudent, middleStudent, lastStudent, notInCollectionStudent };
        string[] elementPosition = { "Перший", "Центральний", "Останній", "Відсутній" };
        
        Stopwatch stopwatch = new Stopwatch();
        Console.WriteLine("\n=== ПОРІВНЯННЯ КОЛЕКЦІЙ ===\n");

        for (int i = 0; i < 4; i++)
        {
            Console.WriteLine($"\n--- Елемент: {elementPosition[i]} ---");

            Console.WriteLine("\n## СТАНДАРТНІ КОЛЕКЦІЇ ##");

            // --- Standard Collections ---

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

            // --- Immutable Collections ---
            Console.WriteLine("\n## НЕЗМІННІ КОЛЕКЦІЇ ##");

            // пошук в ImmutableList<Person>
            stopwatch.Restart();
            bool containsPersonImmutable = _immutableListPerson.Contains(personsToSearch[i]);
            stopwatch.Stop();
            Console.WriteLine($"ImmutableList<Person>.Contains: {stopwatch.ElapsedTicks} тіків, Результат: {containsPersonImmutable}");

            stopwatch.Restart();
            bool containsStringImmutable = _immutableListString.Contains(stringsToSearch[i]);
            stopwatch.Stop();
            Console.WriteLine($"ImmutableList<string>.Contains: {stopwatch.ElapsedTicks} тіків, Результат: {containsStringImmutable}");

            // пошук за ключем у ImmutableDictionary<Person, Student>
            stopwatch.Restart();
            bool containsPersonKeyImmutable = _immutableDictPersonStudent.ContainsKey(personsToSearch[i]);
            stopwatch.Stop();
            Console.WriteLine($"ImmutableDictionary<Person, Student>.ContainsKey: {stopwatch.ElapsedTicks} тіків, Результат: {containsPersonKeyImmutable}");

            // пошук за ключем у ImmutableDictionary<string, Student>
            stopwatch.Restart();
            bool containsStringKeyImmutable = _immutableDictStringStudent.ContainsKey(stringsToSearch[i]);
            stopwatch.Stop();
            Console.WriteLine($"ImmutableDictionary<string, Student>.ContainsKey: {stopwatch.ElapsedTicks} тіків, Результат: {containsStringKeyImmutable}");

            // пошук за значенням у ImmutableDictionary<Person, Student>
            stopwatch.Restart();
            bool containsStudentValueImmutable = _immutableDictPersonStudent.ContainsValue(studentsToSearch[i]);
            stopwatch.Stop();
            Console.WriteLine($"ImmutableDictionary<Person, Student>.ContainsValue: {stopwatch.ElapsedTicks} тіків, Результат: {containsStudentValueImmutable}");

            // --- Sorted Collections ---
            Console.WriteLine("\n## ВІДСОРТОВАНІ КОЛЕКЦІЇ ##");

            // пошук за ключем у SortedList<Person, Student>
            stopwatch.Restart();
            bool containsPersonKeySortedList = _sortedListPersonStudent.ContainsKey(personsToSearch[i]);
            stopwatch.Stop();
            Console.WriteLine($"SortedList<Person, Student>.ContainsKey: {stopwatch.ElapsedTicks} тіків, Результат: {containsPersonKeySortedList}");

            // пошук за ключем у SortedList<string, Student>
            stopwatch.Restart();
            bool containsStringKeySortedList = _sortedListStringStudent.ContainsKey(stringsToSearch[i]);
            stopwatch.Stop();
            Console.WriteLine($"SortedList<string, Student>.ContainsKey: {stopwatch.ElapsedTicks} тіків, Результат: {containsStringKeySortedList}");

            // пошук за значенням у SortedList<Person, Student>
            stopwatch.Restart();
            bool containsStudentValueSortedList = _sortedListPersonStudent.ContainsValue(studentsToSearch[i]);
            stopwatch.Stop();
            Console.WriteLine($"SortedList<Person, Student>.ContainsValue: {stopwatch.ElapsedTicks} тіків, Результат: {containsStudentValueSortedList}");

            // пошук за ключем у SortedDictionary<Person, Student>
            stopwatch.Restart();
            bool containsPersonKeySortedDict = _sortedDictPersonStudent.ContainsKey(personsToSearch[i]);
            stopwatch.Stop();
            Console.WriteLine($"SortedDictionary<Person, Student>.ContainsKey: {stopwatch.ElapsedTicks} тіків, Результат: {containsPersonKeySortedDict}");

            // пошук за ключем у SortedDictionary<string, Student>
            stopwatch.Restart();
            bool containsStringKeySortedDict = _sortedDictStringStudent.ContainsKey(stringsToSearch[i]);
            stopwatch.Stop();
            Console.WriteLine($"SortedDictionary<string, Student>.ContainsKey: {stopwatch.ElapsedTicks} тіків, Результат: {containsStringKeySortedDict}");

            // пошук за значенням у SortedDictionary<Person, Student>
            stopwatch.Restart();
            bool containsStudentValueSortedDict = _sortedDictPersonStudent.ContainsValue(studentsToSearch[i]);
            stopwatch.Stop();
            Console.WriteLine($"SortedDictionary<Person, Student>.ContainsValue: {stopwatch.ElapsedTicks} тіків, Результат: {containsStudentValueSortedDict}");
        }
    }


    public static void RunDemo(int size)
    {
        Console.WriteLine($"Запуск тестування колекцій з розміром {size}");
        TestCollections test = new TestCollections(size);
        test.MeasureSearchTime();
    }
} 