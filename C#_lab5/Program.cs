using Exams;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("===============================================\n");

StudentCollection collection1 = new StudentCollection("Колекція #1");
StudentCollection collection2 = new StudentCollection("Колекція #2");


Journal journal1 = new Journal(); // підписується лише на події колекції #1
Journal journal2 = new Journal(); // підписується на події обох колекцій

// підписка журналу 1 на події колекції 1
collection1.StudentCountChanged += journal1.StudentCountChanged;
collection1.StudentReferenceChanged += journal1.StudentReferenceChanged;

// підписка журналу 2 на події колекції 1 та колекції 2
collection1.StudentCountChanged += journal2.StudentCountChanged;
collection1.StudentReferenceChanged += journal2.StudentReferenceChanged;
collection2.StudentCountChanged += journal2.StudentCountChanged;
collection2.StudentReferenceChanged += journal2.StudentReferenceChanged;

collection1.AddDefaults();

collection2.AddDefaults();

Student newStudent1 = new Student(
    new Person("Олексій", "Коваль", new DateTime(2000, 11, 5)),
    Education.Bachelor,
    302
);
newStudent1.AddExams(
    new Exam("Програмування", 85, new DateTime(2025, 5, 10))
);

Student newStudent2 = new Student(
    new Person("Юлія", "Зінченко", new DateTime(1999, 7, 15)),
    Education.Master,
    502
);
newStudent2.AddExams(
    new Exam("Бази даних", 92, new DateTime(2025, 5, 12))
);

collection1.AddStudents(newStudent1);
collection2.AddStudents(newStudent2);

collection1.Remove(1); 
collection2.Remove(2);


//(1) через індексатор (заміна посилання - генерує подію StudentReferenceChanged)
Student replacementStudent1 = new Student(
    new Person("Віктор", "Петренко", new DateTime(2001, 3, 25)),
    Education.Bachelor,
    303
);
replacementStudent1.AddExams(
    new Exam("Математика", 78, new DateTime(2025, 5, 20))
);

collection1[0] = replacementStudent1;

// (2) зміна властивостей існуючого об'єкта без заміни посилання 
Student existingStudent = collection2[0]; 

existingStudent.PersonInfo = new Person("Наталія", "Савченко", new DateTime(1998, 9, 12));
existingStudent.EducationInfo = Education.Master;
existingStudent.GroupInfo = 504;
existingStudent.Exams.Clear();
existingStudent.AddExams(
    new Exam("Фізика", 89, new DateTime(2025, 5, 18))
);

Console.WriteLine("\nВміст колекції 1 після змін:");
Console.WriteLine(collection1.ToShortString());

Console.WriteLine("\nВміст колекції 2 після змін:");
Console.WriteLine(collection2.ToShortString());

Console.WriteLine("\n===== Журнал 1 (тільки для колекції 1) =====");
Console.WriteLine(journal1);

Console.WriteLine("\n===== Журнал 2 (для обох колекцій) =====");
Console.WriteLine(journal2);

