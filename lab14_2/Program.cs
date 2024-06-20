using AutomobileLibrary;
using lab12_4;

namespace lab14_2
{
    public class Program
    {
        public static Automobile GenerateAuto()
        {
            Random random = new Random();
            int choice = random.Next(1, 4);
            Automobile auto;
            if (choice == 1)
                auto = new Car();
            else if (choice == 2)
                auto = new Suv();
            else
                auto = new Truck();
            auto.RandomInit();
            return auto;
        }

        public static void ShowTable(IEnumerable<Automobile> table)
        {
            foreach (Automobile automobile in table)
            {
                Console.WriteLine(automobile);
            }
            Console.WriteLine();
        }

        public static List<Automobile> GetAutomobilesByBrandAndPriceExt(MyCollection<Automobile> table, string brand, int price)
        {
            return table.Where(t => t.Brand == brand && t.Price > price).ToList();
        }

        public static List<Automobile> FindCarsByPriceAndBrandLinq(MyCollection<Automobile> table, string brand, int price)
        {
            return (from t in table where t.Brand == brand && t.Price > price select t).ToList();
        }

        public static int CountCarsBySeatsExt(MyCollection<Automobile> table, int places)
        {
            return table.Count(t => t is Car p && p.SeatCount == places);
        }

        public static int CountCarsBySeatsLinq(MyCollection<Automobile> table, int places)
        {
            return (from t in table where t is Car p && p.SeatCount == places select t).Count();
        }

        public static double GetAveragePriceByBrandExt(MyCollection<Automobile> table, string brand)
        {
            return table.Where(t => t.Brand == brand).Average(t => t.Price);
        }

        public static double GetAveragePriceByBrandLinq(MyCollection<Automobile> table, string brand)
        {
            return (from t in table where t.Brand == brand select t.Price).Average();
        }

        public static void DisplayDictionary(Dictionary<int, Automobile> dictionary)//Выводит словарь, где ключ - год, а значение - самый дорогой автомобиль этого года.
        {
            foreach (var item in dictionary)
            {
                Console.WriteLine($"Год - {item.Key}");
                item.Value.Show();
                Console.WriteLine();
            }
        }

        public static Dictionary<int, Automobile> GetMostExpensiveCarByYearExt(MyCollection<Automobile> table)
        {
            var res = table.GroupBy(t => t.Year);
            Dictionary<int, Automobile> dictionary = new Dictionary<int, Automobile>();
            foreach (var item in res)
            {
                var sortCars = item.OrderBy(t => t.Price);
                var last = sortCars.Last();

                dictionary[item.Key] = last;
            }
            return dictionary;
        }

        public static Dictionary<int, Automobile> GetMostExpensiveCarByYearLinq(MyCollection<Automobile> table)
        {
            var res = from t in table group t by t.Year;
            Dictionary<int, Automobile> dictionary = new Dictionary<int, Automobile>();
            foreach (var item in res)
            {
                var last = (from t in item orderby t.Price select t).Last();
                dictionary[item.Key] = last;
            }
            return dictionary;
        }

        static void Main(string[] args)
        {
            MyCollection<Automobile> autos = new MyCollection<Automobile>(50);

            int action;
            do
            {
                Console.WriteLine("1.Прибавить объекты");
                Console.WriteLine("2.Напечатать объекты");
                Console.WriteLine("3.Вычисление кол-ва автомобилей заданной модели со стоимостью больше заданной");
                Console.WriteLine("4.Вычисление кол-во легковых автомобилей с заданным кол-вом мест");
                Console.WriteLine("5.Вычисление среднюю стоимость по заданной модели");
                Console.WriteLine("6.Найти самый дорогой автомобиль по всем годам");

                action = IsInt(0, 6);

                if (action == 1)
                {
                    Console.WriteLine("Введите кол-во");
                    int n = int.Parse(Console.ReadLine());

                    for (int i = 0; i < n; i++)
                    {
                        autos.Add(GenerateAuto());
                    }
                }
                else if (action == 2)
                {
                    ShowTable(autos);
                }
                else if (action == 3)
                {
                    Console.WriteLine("Введите модель");
                    string brand = Console.ReadLine();

                    Console.WriteLine("Введите стоимость");

                    int price = int.Parse(Console.ReadLine());

                    var res1 = GetAutomobilesByBrandAndPriceExt(autos, brand, price);
                    ShowTable(res1);

                    var res2 = FindCarsByPriceAndBrandLinq(autos, brand, price);
                    ShowTable(res2);
                }
                else if (action == 4)
                {
                    Console.WriteLine("Введите кол-во мест");
                    int countPlaces = int.Parse(Console.ReadLine());

                    int count1 = CountCarsBySeatsExt(autos, countPlaces);
                    Console.WriteLine("Кол-во = " + count1);

                    int count2 = CountCarsBySeatsLinq(autos, countPlaces);
                    Console.WriteLine("Кол-во = " + count1);
                }
                else if (action == 5)
                {
                    Console.WriteLine("Введите модель");
                    string brand = Console.ReadLine();

                    double avg1 = GetAveragePriceByBrandExt(autos, brand);
                    Console.WriteLine("Результат = " + avg1);

                    double avg2 = GetAveragePriceByBrandLinq(autos, brand);
                    Console.WriteLine("Результат = " + avg2);
                }
                else if (action == 6)
                {
                    var res1 = GetMostExpensiveCarByYearExt(autos);
                    DisplayDictionary(res1);

                    Console.WriteLine();

                    var res2 = GetMostExpensiveCarByYearLinq(autos);
                    DisplayDictionary(res2);
                }

            } while (true);
        }

        static int IsInt(int min, int max) //функция для проверки на Int (параметры - минимальное и максимальное значение)
        {
            bool isConvert;
            int number;
            do
            {
                string buf = Console.ReadLine();
                isConvert = int.TryParse(buf, out number);
                if (!isConvert || number < min || number > max)
                {
                    Console.WriteLine($"Неправильно введено число. Введите значение от {min} до {max}");
                }
            } while (!isConvert || number < min || number > max);
            return number;
        }
    }
}
