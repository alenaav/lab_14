using AutomobileLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace lab14_1
{
    public class Program
    {
        // Метод для заполнения очереди списков автомобилей случайными данными
        public static void FillQueue(Queue<List<Automobile>> queue)
        {
            Random random = new Random();
            int count = random.Next(5, 10); // Определяем случайное количество списков в очереди

            for (int i = 0; i < count; i++)
            {
                int countAuto = random.Next(3, 10); // Определяем случайное количество автомобилей в каждом списке
                List<Automobile> autos = new List<Automobile>();
                for (int j = 0; j < countAuto; j++)
                {
                    int choice = random.Next(1, 4); // Случайно выбираем тип автомобиля
                    Automobile automobile;
                    if (choice == 1)
                        automobile = new Car(); //  легковой автомобиль
                    else if (choice == 2)
                        automobile = new Suv(); //  внедорожник
                    else
                        automobile = new Truck(); // грузовик

                    automobile.RandomInit(); // Инициализируем случайными данными
                    autos.Add(automobile); // Добавляем автомобиль в список
                }
                queue.Enqueue(autos); // Добавляем список автомобилей в очередь
            }
        }

        // Метод для вывода на экран все автомобили из очереди и их информацию, группируя их по спискам
        public static void Show(Queue<List<Automobile>> queue)
        {
            int i = 1;
            foreach (var item in queue)
            {
                foreach (var car in item)
                {
                    car.Show();
                    Console.WriteLine();
                }
                i++;
                Console.WriteLine();
            }
        }

        // Метод для отображения только легковых автомобилей из всех цехов, используя метод расширения
        public static void ShowTruckExtension(Queue<List<Automobile>> queue)
        {
            Console.WriteLine("Метод расширения");
            var result = queue.SelectMany(t => t).Where(t => t is Car); // Выбираем все легковые автомобили
            foreach (var item in result)
            {
                item.Show();
                Console.WriteLine();
            }
        }

        // Метод для отображения только легковых автомобилей из всех цехов, используя LINQ-запрос
        public static void ShowTruckLINQ(Queue<List<Automobile>> queue)
        {
            Console.WriteLine("LINQ");
            var result = from t in queue.SelectMany(t => t) where t is Car select t; // LINQ-запрос для выбора легковых автомобилей
            foreach (var item in result)
            {
                item.Show();
                Console.WriteLine();
            }
        }

        // Метод для нахождения и отображения цеха с максимальным количеством автомобилей, используя метод расширения
        public static void FindQueueMaxAutoExtension(Queue<List<Automobile>> queue)
        {
            int maxCount = queue.Max(t => t.Count); // Максимальное количество автомобилей в одном списке
            var maxQueue = queue.First(t => t.Count == maxCount); // Первый список с максимальным количеством автомобилей
            int index = queue.ToList().IndexOf(maxQueue); // Номер этого списка

            Console.WriteLine($"Максимальное количество авто = {maxCount}, номер цеха = {index + 1}");
        }

        // Метод для нахождения и отображения цеха с максимальным количеством автомобилей, используя LINQ-запрос
        public static void FindQueueMaxAutoLINQ(Queue<List<Automobile>> queue)
        {
            int maxCount = (from list in queue select list.Count).Max(); // Максимальное количество автомобилей в одном списке
            var maxQueue = (from list in queue where list.Count == maxCount select list).First(); // Первый список с максимальным количеством автомобилей
            int index = queue.ToList().IndexOf(maxQueue); // Номер этого списка

            Console.WriteLine($"Максимальное количество авто = {maxCount}, номер цеха = {index + 1}");
        }

        [ExcludeFromCodeCoverage]
        // Метод для группировки автомобилей по бренду и отображения количества каждого бренда, используя метод расширения
        public static void GroupExtension(Queue<List<Automobile>> queue)
        {
            var result = queue.SelectMany(t => t).GroupBy(t => t.Brand); // Группировка по бренду
            foreach (var item in result)
            {
                Console.WriteLine($"Марка авто - {item.Key}, Количество - {item.Count()}");
            }
        }

        [ExcludeFromCodeCoverage]
        // Метод для группировки автомобилей по бренду и отображения количества каждого бренда, используя LINQ-запрос
        public static void GroupLINQ(Queue<List<Automobile>> queue)
        {
            var result = (from auto in queue.SelectMany(t => t) group auto by auto.Brand); // Группировка по бренду
            foreach (var item in result)
            {
                Console.WriteLine($"Марка авто - {item.Key}, Количество - {item.Count()}");
            }
        }

        [ExcludeFromCodeCoverage]
        // Метод для объединения первого и последнего списков автомобилей и отображения результата, используя метод расширения
        public static void UnionExtension(Queue<List<Automobile>> queue)
        {
            var result = queue.First().Union(queue.Last()); // Объединение первого и последнего списков
            foreach (var item in result)
            {
                item.Show();
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        [ExcludeFromCodeCoverage]
        // Метод для объединения первого и последнего списков автомобилей и отображения результата, используя LINQ-запрос
        public static void UnionLINQ(Queue<List<Automobile>> queue)
        {
            var result = (from t in queue.First() select t).Union(from p in queue.Last() select p); // Объединение первого и последнего списков
            foreach (var item in result)
            {
                item.Show();
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        // Метод для заполнения списка студентов случайными данными
        public static void FillListStudents(List<Student> students)
        {
            Random random = new Random();
            int count = random.Next(5, 10); // Определяем случайное количество студентов
            for (int i = 0; i < count; i++)
            {
                Student student = new Student();
                student.RandomInit(); // Инициализируем случайными данными

                students.Add(student); // Добавляем студента в список
            }
        }

        // Метод для отображения списка студентов
        public static void ShowStudents(List<Student> students)
        {
            foreach (var student in students)
            {
                Console.WriteLine(student.ToString()); // Показать информацию о студенте
            }
        }

        // Метод для объединения студентов с автомобилями по бренду и отображения результата, используя метод расширения
        public static void JoinStudentsAutoExtension(List<Student> students, Queue<List<Automobile>> queue)
        {
            var res = students.Join(
                queue.SelectMany(t => t),
                p => p.AutoBrand,
                v => v.Brand,
                (p, v) => new { Name = p.Name, Brand = p.AutoBrand, Price = v.Price }
            );

            foreach (var item in res);
        }

        // Метод для объединения студентов с автомобилями по бренду и отображения результата, используя LINQ-запрос
        public static void JoinStudentsAutoLINQ(List<Student> students, Queue<List<Automobile>> queue)
        {
            var res = from student in students
                      join c in queue.SelectMany(t => t) on student.AutoBrand equals c.Brand
                      select new { Name = student.Name, Brand = student.AutoBrand, Price = c.Price };

            foreach (var item in res)
            {
                Console.WriteLine($"Имя: {item.Name}, Модель: {item.Brand}, Цена: {item.Price}");
            }
            Console.WriteLine();
        }



        static void Main(string[] args)
        {
            Queue<List<Automobile>> queue = new Queue<List<Automobile>>(); // Создание очереди списков автомобилей
            FillQueue(queue); // Заполнение очереди

            // Цикл для выбора действия из меню
            do
            {
                // Меню 
                Console.WriteLine("1.Напечатать на экран");
                Console.WriteLine("2.Вывести только легковые авто из всех цехов");
                Console.WriteLine("3.Цех с максимальным количеством автомобилей");
                Console.WriteLine("4.Вывести количество автомобилей каждого бренда");
                Console.WriteLine("5.Объединение первого и последнего цеха");
                Console.WriteLine("6.Соединение студентов с автомобилями");

                int action = IsInt(1, 6); // ввод номера действия с консоли
                if (action == 1)
                {
                    Show(queue); // Показать все автомобили
                }
                else if (action == 2)
                {
                    ShowTruckExtension(queue); // Показать только легковые автомобили
                    ShowTruckLINQ(queue);
                }
                else if (action == 3)
                {
                    Console.WriteLine("Метод расширения");
                    FindQueueMaxAutoExtension(queue); // Найти цех с максимальным количеством автомобилей
                    Console.WriteLine("LINQ");
                    FindQueueMaxAutoLINQ(queue);
                }
                else if (action == 4)
                {
                    Console.WriteLine("Метод расширения");
                    GroupExtension(queue); // Группировка автомобилей по бренду
                    Console.WriteLine("LINQ");
                    GroupLINQ(queue);
                }
                else if (action == 5)
                {
                    Console.WriteLine("Метод расширения");
                    UnionExtension(queue); // Объединение первого и последнего цеха
                    Console.WriteLine("LINQ");
                    UnionLINQ(queue);
                }
                else if (action == 6)
                {
                    List<Student> students = new List<Student>();
                    FillListStudents(students); // Заполнение списка студентов
                    //ShowStudents(students);

                    Console.WriteLine("Метод расширения");
                    JoinStudentsAutoExtension(students, queue); // Объединение студентов с автомобилями
                    Console.WriteLine("LINQ");
                    JoinStudentsAutoLINQ(students, queue);
                }

            } while (true); // Повторять меню, пока пользователь не завершит программу
        }



        // Функция для проверки ввода числа
        public static int IsInt(int min, int max)
        {
            bool isConvert;
            int number;
            do
            {
                string buf = Console.ReadLine();
                isConvert = int.TryParse(buf, out number); // Проверка, является ли ввод числом
                if (!isConvert || number < min || number > max)
                {
                    Console.WriteLine($"Неправильно введено число. Введите значение от {min} до {max}");
                }
            } while (!isConvert || number < min || number > max);
            return number;
        }
    }
}
