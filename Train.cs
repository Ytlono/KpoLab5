using Lab5;
using System.Xml.Serialization;

namespace Lab5
{
    
    public class Train
    {
        [XmlIgnore]
        private List<Carriage> carriages ;

        [XmlIgnore]
        private TrainSerializer trainSerializer = new TrainSerializer();

        [XmlIgnore]
        private int allPassengerCount;

        [XmlIgnore]
        private double allBaggageCount;

        [XmlIgnore]
        private double trainDistance;
        [XmlIgnore]
        private double maxDistance;

        [XmlArray("Train")]
        [XmlArrayItem("PassengerCarriage",typeof(PassengerCarriage))]
        [XmlArrayItem("BaggageCarriage", typeof(BaggageCarriage))]
        public List<Carriage> Carriages { 
            get{ return carriages; } 
            set{ carriages = value; }
        }
        public double TrainDistance
        {
            get { return trainDistance; }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("Ошибка: Пройденное расстояние не может быть отрицательным. Установлено значение 0.");
                    trainDistance = 0;
                }
                else if (value > MaxDistance)
                {
                    Console.WriteLine($"Ошибка: Пройденное расстояние не может превышать максимальное значение ({MaxDistance}). Установлено максимальное значение.");
                    trainDistance = MaxDistance;
                }
                else
                {
                    trainDistance = value;
                }
            }
        }

        public double MaxDistance
        {
            get { return maxDistance; }
            set { maxDistance = value; }

        }
        public Train()
        {
            Carriages = new List<Carriage>();
        }


        [XmlIgnore]
        public int AllPassengerCount
        {
            get
            {
                return Carriages
                    .OfType<PassengerCarriage>()
                    .Sum(carriage => carriage.PassengerCount);
            }
        }

        [XmlIgnore]
        public double AllBaggageCount
        {
            get
            {
                return Carriages
                    .OfType<BaggageCarriage>()
                    .Sum(carriage => carriage.TotalBaggageWeight);
            }
        }


        public void FillCarriageInformation()
        {
            Console.Write("Количество пассажирских вагонов: ");
            int passengerCount = ConvertToInt(Console.ReadLine());
            Console.WriteLine("----------------------------------------------->");
            AddPassengerCarriage(passengerCount);


            Console.WriteLine("\n----------------------------------------------->");
            Console.Write("Количество багажных вагонов: ");
            int baggageCount = ConvertToInt(Console.ReadLine());
            AddBaggageCarriage(baggageCount);


            Console.WriteLine("----------------------------------------------->");
            Console.ReadKey(true);
        }

        public void AddPassengerCarriage(int count)
        {
            for (int i = 0; i < count; i++)
            {
                PassengerCarriage passengerCarriage = PassengerCarriage.Set();
                passengerCarriage.CountPrice();
                Carriages.Add(passengerCarriage);
            }

            CheckCarriagesActivity();
            UpdateTrainDistance();
        }
        
        public void AddBaggageCarriage(int count)
        {
            for (int i = 0; i < count; i++)
            {
                BaggageCarriage baggageCarriage = BaggageCarriage.Set();
                baggageCarriage.CountPrice();
                Carriages.Add(baggageCarriage);
            }

            CheckCarriagesActivity();
            UpdateTrainDistance();
        }

        public void ShowCarriages()
        {
            SortCarriagesByDistance();
            if (Carriages.Count != 0)
            {
                Console.WriteLine("Список вагонов:");
                foreach (var carriage in Carriages)
                {
                    Console.WriteLine(carriage);
                    Console.WriteLine("----------------------------------------------->");
                }
            }
            else
            {
                Console.WriteLine("Поезд пуст.");
                Console.WriteLine("----------------------------------------------->");
            }
        }

        public void ShowCarriages(int key)
        {
            foreach (var carriage in Carriages)
            {
                if (carriage is PassengerCarriage passengerCarriage)
                {
                    Console.WriteLine(passengerCarriage);
                    Console.WriteLine("----------------------------------------------->");
                }
            }
            Console.ReadKey(true);
        }

        public void CountTotalPrice()
        {
            double totalPrice = 0;
            foreach (Carriage carriage in Carriages)
            {
                if (carriage is PassengerCarriage passengerCarriage)
                {
                    totalPrice = +passengerCarriage.FinalPrice;
                }
                else if (carriage is BaggageCarriage baggageCarriage)
                {
                    totalPrice = +baggageCarriage.BaseTransportPrice;
                }
            }
            Console.WriteLine(totalPrice);

            Console.WriteLine("----------------------------------------------->");
            Console.ReadKey(true);
        }

        public void ShowSortedPassengerCarriages()
        {
            Carriages.OfType<PassengerCarriage>()
                     .OrderBy(carriage => carriage)
                     .ToList();

            Console.WriteLine("Сортировка пассажирских вагонов по комфорту:");
            Console.WriteLine("----------------------------------------------->");
            ShowCarriages(1);
            SortCarriagesByDistance();
        }

        public void UpdateTrainDistance()
        {
            if (Carriages.Count == 0)
            {
                Console.WriteLine("Поезд не содержит вагонов.");
                return;
            }

            double maxCarriageDistance = Carriages.Max(carriage => carriage.Distance);

            MaxDistance = maxCarriageDistance;
        }

        public int ConvertToInt(string input)
        {
            int result;
            while (!int.TryParse(input, out result))
            {
                Console.Write("Ошибка ввода. Пожалуйста, введите целое число: ");
                input = Console.ReadLine();
            }
            return result;
        }
        public void CheckCarriagesActivity()
        {
            foreach (var carriage in Carriages)
            {
                if (carriage.Distance < TrainDistance)
                {
                    carriage.Status = CarriageStatus.Inactive;
                    Console.WriteLine($"Вагон с ID {carriage.CarriageId} деактивирован, так как его дистанция {carriage.Distance} км больше текущей пройденной дистанции поезда ({TrainDistance} км).");
                }
                else
                {
                    carriage.Status = CarriageStatus.Active;
                    Console.WriteLine($"Вагон с ID {carriage.CarriageId} активен.");
                }
            }
        }
        public void ChangeTrainDistance()
        {
            Console.WriteLine($"Текущая пройденная дистанция: {TrainDistance} км.");
            Console.WriteLine($"Максимально возможная дистанция: {MaxDistance} км.");
            Console.Write("Введите новую пройденную дистанцию: ");
            double newDistance = Convert.ToDouble(Console.ReadLine());

            if (newDistance < 0)
            {
                Console.WriteLine("Ошибка: Пройденная дистанция не может быть отрицательной.");
            }
            else if (newDistance > MaxDistance)
            {
                Console.WriteLine($"Ошибка: Пройденная дистанция не может быть больше максимальной ({MaxDistance} км).\nУстановлено значение равное максимальной дистанции.");
                TrainDistance = MaxDistance;
            }
            else
            {
                TrainDistance = newDistance;
                Console.WriteLine($"Новая пройденная дистанция установлена: {TrainDistance} км.");
            }


            Console.WriteLine("----------------------------------------------->");
            Console.ReadKey(true);
            CheckCarriagesActivity();
        }


        public void PassengerAndBaggageCounts()
        {
            Console.WriteLine("Количество пассажиров: " + AllPassengerCount);
            Console.WriteLine("Общий вес багажа: " + AllBaggageCount);

            Console.WriteLine("----------------------------------------------->");
            Console.ReadKey(true);
        }
        public void RemoveCarriageById()
        {
            ShowCarriages(); 

            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Удалить последний вагон");
            Console.WriteLine("2. Удалить все неактивные вагоны");
            Console.WriteLine("3. Деактивировать вагон по ID");
            int choice = ConvertToInt(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    if (Carriages.Count > 0)
                    {
                        var lastCarriage = Carriages.Last();
                        Carriages.Remove(lastCarriage);
                        Console.WriteLine($"Последний вагон с ID {lastCarriage.CarriageId} удален.");
                    }
                    else
                    {
                        Console.WriteLine("Нет вагонов для удаления.");
                    }
                    break;

                case 2:
                    var inactiveCarriages = Carriages.Where(carriage => carriage.Status == CarriageStatus.Inactive && carriage.Distance < TrainDistance).ToList();
                    foreach (var carriage in inactiveCarriages)
                    {
                        Carriages.Remove(carriage);
                        Console.WriteLine($"Вагон с ID {carriage.CarriageId} удален.");
                    }
                    break;

                case 3:
                    Console.Write("Введите ID вагона для деактивации: ");
                    int carriageId = ConvertToInt(Console.ReadLine());
                    var carriageToDeactivate = Carriages.FirstOrDefault(c => c.CarriageId == carriageId);
                    if (carriageToDeactivate != null)
                    {
                        carriageToDeactivate.Status = CarriageStatus.Inactive;
                        carriageToDeactivate.Distance = 0; 
                        Carriages.Remove(carriageToDeactivate);
                        Carriages.Add(carriageToDeactivate); 
                        Console.WriteLine($"Вагон с ID {carriageId} деактивирован и перемещен в конец.");
                    }
                    else
                    {
                        Console.WriteLine($"Вагон с ID {carriageId} не найден.");
                    }
                    break;

                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }

            UpdateTrainDistance();
            Console.WriteLine("----------------------------------------------->");
            Console.ReadKey(true);
        }

        public void SortCarriagesByDistance()
        {
            var sortedCarriages = Carriages.OrderByDescending(carriage => carriage).ToList();
            Carriages = sortedCarriages;
        }

        public void LoadFromXml(string filePath)
        {
            Train loadedTrain = trainSerializer.DeserializeTrain(filePath);
            Carriages = loadedTrain.Carriages;
            UpdateTrainDistance();

            Console.WriteLine("----------------------------------------------->");
            Console.ReadKey(true);
        }

        public void SaveToXml(Train train, string filePath)
        {
            trainSerializer.SerializeTrain(train,filePath);

            Console.WriteLine("----------------------------------------------->");
            Console.ReadKey(true);
        }
    }
}
