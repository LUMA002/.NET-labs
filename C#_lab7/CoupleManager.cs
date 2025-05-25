using System;
using System.Linq;
using System.Reflection;

namespace Lab_7
{
    public static class CoupleManager
    {
        private static readonly Random _random = new Random();

        public static IHasName Couple(Human first, Human second)
        {

            if (first.IsMale == second.IsMale)
            {
                throw new SameGenderException();
            }

            // перевірка симпатії першого до другого
            bool firstLikesSecond = CheckMutualAttraction(first, second);
            if (!firstLikesSecond)
            {
                return null;
            }

            // перевірка симпатії другого до першого
            bool secondLikesFirst = CheckMutualAttraction(second, first);
            if (!secondLikesFirst)
            {
                return null;
            }

            // отримання імені від другого партнера
            string childName = GetNameFromPartner(second);

            // створення дочірнього об'єкта
            return CreateChild(first, second, childName);
        }

        private static bool CheckMutualAttraction(Human person, Human partner)
        {
            var enumerator = new CoupleAttributeEnumerator(person.GetType());

            while (enumerator.MoveNext())
            {
                var attribute = enumerator.Current;
                if (attribute.Pair == partner.GetType().Name)
                {
                    // від 0 до 1 - ймовірність симпатії
                    double randomValue = _random.NextDouble();
                    return randomValue <= attribute.Probability;
                }
            }

            return false;
        }

        private static string GetNameFromPartner(Human partner)
        {
            try
            {
                var methods = partner.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
                var stringMethod = methods.FirstOrDefault(m => m.ReturnType == typeof(string) && m.GetParameters().Length == 0);

                if (stringMethod != null)
                {
                    return stringMethod.Invoke(partner, null) as string;
                }
            }
            catch
            {
            }

            return "DefaultName";
        }

        private static IHasName CreateChild(Human first, Human partner, string childName)
        {
            var enumerator = new CoupleAttributeEnumerator(first.GetType());

            while (enumerator.MoveNext())
            {
                var attribute = enumerator.Current;
                if (attribute.Pair == partner.GetType().Name)
                {
                    // дочірній об'єкт
                    Type childType = Type.GetType($"Lab_7.{attribute.ChildType}");
                    if (childType != null)
                    {
                        var child = Activator.CreateInstance(childType, childName) as IHasName;

                        //  Human, встановлюємо MiddleName
                        if (child is Human humanChild)
                        {
                            //  по батькові від батька
                            var father = first.IsMale ? first : partner;
                            SetMiddleName(humanChild, father);
                        }

                        return child;
                    }
                    break;
                }
            }

            return null;
        }

        private static void SetMiddleName(Human child, Human father)
        {
            var middleNameProperty = child.GetType().GetProperty("MiddleName");
            if (middleNameProperty != null && middleNameProperty.CanWrite)
            {
                string middleName = CreateMiddleName(father.Name, child.IsMale);
                middleNameProperty.SetValue(child, middleName);
            }
        }

        private static string CreateMiddleName(string fatherName, bool isChildMale)
        {
            if (string.IsNullOrEmpty(fatherName))
                return "";

            return isChildMale ? fatherName + "ович" : fatherName + "івна";
        }
    }
}