namespace Lab5
{
    public class Lab5
    {
        public static Train train = new Train();
        public static string xmlFileName = "Trains.Xml";

        public static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("----------------------------------------------->");
                Console.WriteLine("|MENU");
                Console.WriteLine("1 Работа с XML файлом");
                Console.WriteLine("2 Добавить вагоны");
                Console.WriteLine("3 Показать все вагоны");
                Console.WriteLine("4 Посчитать общую стоимость");
                Console.WriteLine("5 Сортировка по уровню комфортности");
                Console.WriteLine("6 Общее количество пассажиров и вес багажа");
                Console.WriteLine("7 Удаление Вагонов");
                Console.WriteLine("8 Изменить пройденную дистанцию");
                Console.WriteLine("[ESC] Выход");
                Console.WriteLine("----------------------------------------------->");

                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        HandleSerializationDeserialization();
                        break;
                    case ConsoleKey.D2:
                        train.FillCarriageInformation();
                        train.SortCarriagesByDistance();
                        break;
                    case ConsoleKey.D3:
                        train.ShowCarriages();
                        Console.ReadKey(true);
                        break;
                    case ConsoleKey.D4:
                        train.CountTotalPrice();
                        break;
                    case ConsoleKey.D5:
                        train.ShowSortedPassengerCarriages();
                        break;
                    case ConsoleKey.D6:
                        train.PassengerAndBaggageCounts();
                        break;
                    case ConsoleKey.D7:
                        train.RemoveCarriageById();
                        train.SortCarriagesByDistance();
                        break;
                    case ConsoleKey.D8:
                        train.ChangeTrainDistance();
                        break;
                    default:
                        Console.WriteLine("Некорректный ввод. Пожалуйста, выберите номер из меню.");
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        public static void HandleSerializationDeserialization()
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 - Загрузить данные из XML");
            Console.WriteLine("2 - Сохранить данные в XML");
            Console.WriteLine("----------------------------------------------->");
            Console.Write("Ваш выбор: ");

            int action = train.ConvertToInt(Console.ReadLine());

            switch (action)
            {
                case 1:
                    train.LoadFromXml(xmlFileName);
                    break;

                case 2:
                    train.SaveToXml(train, xmlFileName);
                    break;

                default:
                    Console.ReadKey(true);
                    break;
            }
        }
    }
}
