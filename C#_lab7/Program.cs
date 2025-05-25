using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_7
{
    class Program
    {
        private static readonly Random _random = new Random();
        private static readonly List<Human> _people = new List<Human>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            // Перевірка на неділю
            //if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            //{
            //    Console.WriteLine("Консоль не працює по неділях!");
            //    return;
            //}

            InitializePeople();

            Console.WriteLine("Натисніть Enter для нової пари, Q або F10 для виходу:");

            while (true)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Q || key.Key == ConsoleKey.F10)
                {
                    break;
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    ProcessCouple();
                }
            }
        }

        private static void InitializePeople()
        {
            _people.Add(new Student("Іван"));
            _people.Add(new Botan("Олег"));
            _people.Add(new Girl("Марія"));
            _people.Add(new PrettyGirl("Ольга"));
            _people.Add(new SmartGirl("Анна"));
        }

        private static void ProcessCouple()
        {
            try
            {
                // різні випадкові люди
                var firstIndex = _random.Next(_people.Count);
                var secondIndex = _random.Next(_people.Count);

                while (secondIndex == firstIndex)
                {
                    secondIndex = _random.Next(_people.Count);
                }

                var first = _people[firstIndex];
                var second = _people[secondIndex];

                Console.WriteLine($"\nЗустрічаються: {first.GetType().Name} {first.Name} і {second.GetType().Name} {second.Name}");

                var result = CoupleManager.Couple(first, second);

                if (result != null)
                {
                    Console.WriteLine($"Успіх! Створено: {result.GetType().Name} з ім'ям {result.Name}");
                    if (result is Human human && !string.IsNullOrEmpty(human.MiddleName))
                    {
                        Console.WriteLine($"По батькові: {human.MiddleName}");
                    }
                }
                else
                {
                    Console.WriteLine("Не сподобались один одному");
                }
            }
            catch (SameGenderException)
            {
                Console.WriteLine("Помилка: Зустрілися дві людини однакової статі!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
    }
}