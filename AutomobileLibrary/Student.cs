using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileLibrary
{
    public class Student : IInit
    {
        public string name;
        public int age;
        public double gpa;
        public static int counter = 0;

        public string Name
        {
            get
            { return name; }
            set
            { name = value; }
        }
        public int Age
        {
            get { return age; }
            set
            {
                if (value < 0)
                {
                    age = 0;
                }
                else
                if (value <= 50)
                {
                    age = value;
                }
                else
                {
                    age = value - 50;
                }
            }
        }
        public double GPA
        {
            get => gpa;
            set
            {
                if (value < 0)
                {
                    gpa = 0;
                }
                else
                if (value <= 10)
                {
                    gpa = value;
                }
                else
                {
                    gpa = value - 10;
                }
            }
        }

        public string AutoBrand { get; set; }

        public Student() //конструктор без параметров
        {
            Name = "NoName";
            Age = 0;
            GPA = 0.0;
            counter++;
        }

        public Student(string name, int age, double gpa) //конструктор с параметрами
        {
            Name = name;
            Age = age;
            GPA = gpa;
            counter++;
        }

        public Student(Student otherStudent) //конструктор копирования
        {
            Name = otherStudent.name;
            Age = otherStudent.age;
            GPA = otherStudent.gpa;
            counter++;
        }

        [ExcludeFromCodeCoverage]
        public void Show() //метод для вывода информации
        {
            Console.WriteLine("Имя: " + Name);
            Console.WriteLine("Возраст: " + Age);
            Console.WriteLine("GPA: " + GPA);
        }

        public string CompareWith(Student otherStudent) //нестатический метод для сравнивания студентов по их показателям
        {
            string result = Name + (Age < otherStudent.Age ? " младше " : (Age > otherStudent.Age ? " старше " : " ровесник ")) + otherStudent.Name;
            result += ". GPA " + Name + (GPA > otherStudent.GPA ? " выше " : (GPA < otherStudent.GPA ? " ниже " : " равен ")) + "GPA " + otherStudent.Name;
            return result;
        }

        public static void CompareStudents(Student s1, Student s2) //статическая
        {
            string result = s1.Name + (s1.Age < s2.Age ? " младше " : (s1.Age > s2.Age ? " старше " : " ровесник ")) + s2.Name;
            result += ". GPA " + s1.Name + (s1.GPA > s2.GPA ? " выше " : (s1.GPA < s2.GPA ? " ниже " : " равен ")) + "GPA " + s2.Name;
            Console.WriteLine(result);
        }

        public static Student operator --(Student a) //унарные операции
        {
            string formattedName = char.ToUpper(a.Name[0]) + a.Name.Substring(1).ToLower();
            return new Student(formattedName, a.Age, a.GPA);
        }
        public static Student operator ++(Student a)
        {
            a.Age++;
            return a;
        }

        //операции приведения типа
        public static explicit operator int(Student s)//явная
        {
            if (s.Age >= 18 && s.Age <= 22)
            {
                return s.Age - 17;
            }
            else
            {
                return -1;
            }
        }
        public static implicit operator bool(Student s)
        {
            if (s.GPA < 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Student operator %(Student s, string newName) //бинарные операции
        {
            return new Student(newName, s.Age, s.GPA);
        }
        public static Student operator -(Student s, double d)
        {
            double newGpa = s.GPA - d;
            if (newGpa < 0)
            {
                newGpa = 0;
            }
            return new Student(s.Name, s.Age, newGpa);
        }

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"Имя студента: {Name}, возраст студента: {Age}, GPA студента: {GPA}";
        }

        [ExcludeFromCodeCoverage]
        public virtual void Init()
        {
            Console.WriteLine("Введите имя студента:");
            Console.ReadLine();
            Console.WriteLine("Введите возраст студента:");
            try
            {
                Age = int.Parse(Console.ReadLine());
            }
            catch
            {
                Age = 18;
            }

            Console.WriteLine("Введите GPA cтудента:");
            try
            {
                GPA = double.Parse(Console.ReadLine());
            }
            catch
            {
                GPA = 5.5;
            }
        }

        public virtual void RandomInit()
        {
            string[] names = {
                "Данил",
                "Алексей",
                "Надежда",
                "Вера",
                "Любовь",
                "Ольга",
                "Мария",
                "Демид",
                "Мухаммед"
            };

            string[] brands = { "BMW", "Mercedes-Benz", "Renault", "Hyundai", "Ford", "Volkswagen", "Lamborghini", "Ferrari", "Alfa-Romeo" };

            Random rnd = new Random();
            Name = names[rnd.Next(names.Length)];
            Age = rnd.Next(18, 22);
            GPA = rnd.Next(0, 10);
            AutoBrand = brands[rnd.Next(brands.Length)];
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is not Student) return false;
            return ((Student)obj).Name == this.Name && ((Student)obj).Age == this.Age && ((Student)obj).GPA == this.GPA;
        }
    }
}
