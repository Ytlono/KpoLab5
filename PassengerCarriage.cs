using System;
using System.Xml.Serialization;

namespace Lab5
{
    public enum ComfortLevel
    {
        Economy = 1,
        Standard = 2,
        Business = 3,
        Luxury = 4
    }

    public class PassengerCarriage : Carriage,IComparable<PassengerCarriage>
    {

        [XmlIgnore]
        private int passengerCapacity;

        [XmlIgnore]
        private int passengerCount;

        [XmlIgnore]
        private ComfortLevel comfort;

        [XmlIgnore]
        private bool hasDiningFacilities;

        [XmlIgnore]
        private bool hasWiFi;

        [XmlIgnore]
        private double finalPrice;

        
        [XmlElement("PassengerCapacity")]
        public int PassengerCapacity
        {
            get { return passengerCapacity; }
            set { passengerCapacity = value; }
        }


        [XmlElement("PassengerCount")]
        public int PassengerCount
        {
            get { return passengerCount; }
            set { passengerCount = value; }
        }


        [XmlElement("Comfort")]
        public ComfortLevel Comfort
        {
            get { return comfort; }
            set { comfort = value; }
        }


        [XmlElement("HasDiningFacilities")]
        public bool HasDiningFacilities
        {
            get { return hasDiningFacilities; }
            set { hasDiningFacilities = value; }
        }


        [XmlElement("HasWiFi")]
        public bool HasWiFi
        {
            get { return hasWiFi; }
            set { hasWiFi = value; }
        }


        [XmlElement("FinalPrice")]
        public double FinalPrice
        {
            get { return finalPrice; }
            set { finalPrice = value; }
        }

        public PassengerCarriage() { }
        public PassengerCarriage(int carriageId, double distance, double baseTransportPrice, ComfortLevel comfort, int passengerCapacity, bool hasDiningFacilities, bool hasWiFi)
            : base(carriageId, distance, baseTransportPrice)
        {
            PassengerCapacity = passengerCapacity;
            Comfort = comfort;
            HasDiningFacilities = hasDiningFacilities;
            HasWiFi = hasWiFi;
        }

        public static PassengerCarriage Set()
        {
            PassengerCarriage passengerCarriage = new PassengerCarriage(0, 0.0, 0, ComfortLevel.Standard, 50, false, true);

            Carriage.SetBaseCarriageFields(passengerCarriage);

            Console.Write("Выберите класс вагона\n1 - Эконом\n2 - Стандарт\n3 - Бизнес\n4 - Люкс\n:");
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.KeyChar)
                {
                    case '1':
                        passengerCarriage.Comfort = ComfortLevel.Economy;
                        break;
                    case '2':
                        passengerCarriage.Comfort = ComfortLevel.Standard;
                        break;
                    case '3':
                        passengerCarriage.Comfort = ComfortLevel.Business;
                        break;
                    case '4':
                        passengerCarriage.Comfort = ComfortLevel.Luxury;
                        break;
                    default:
                        Console.Write("\nВведите корректное число:");
                        continue;
                }
                Console.WriteLine(passengerCarriage.Comfort);
                break;
            }

            Console.Write("\nВведите количество мест в вагоне (MAX 50):");
            while (true)
            {
                passengerCarriage.PassengerCapacity = Convert.ToInt32(Console.ReadLine());
                if (passengerCarriage.PassengerCapacity > 0 && passengerCarriage.PassengerCapacity <= 50)
                {
                    Console.Write("");
                    break;
                }
                Console.Write("Ошибка некорректный ввод:");
            }

            Console.Write("\nВведите количество занятых мест (не больше вместимости):");
            while (true)
            {
                passengerCarriage.PassengerCount = Convert.ToInt32(Console.ReadLine());
                if (passengerCarriage.PassengerCount >= 0 && passengerCarriage.PassengerCount <= passengerCarriage.PassengerCapacity)
                {
                    break;
                }
                Console.Write("Ошибка некорректный ввод, повторите:");
            }

            if (passengerCarriage.Comfort > ComfortLevel.Standard)
            {
                passengerCarriage.HasWiFi = true;
                passengerCarriage.HasDiningFacilities = true;

                Console.WriteLine("\nНаличие Wi-Fi: Да");
                Console.WriteLine("\nСтоловая: Да");
            }
            else
            {
                Console.WriteLine("\nВыберите наличие Wi-Fi (1 - Да, 2 - Нет):");
                ConsoleKeyInfo wifiKey = Console.ReadKey(true);
                switch (wifiKey.KeyChar)
                {
                    case '1':
                        passengerCarriage.HasWiFi = true;
                        Console.WriteLine("\nWi-Fi: Да");
                        break;
                    case '2':
                        passengerCarriage.HasWiFi = false;
                        Console.WriteLine("\nWi-Fi: Нет");
                        break;
                    default:
                        Console.WriteLine("\nНеверный выбор. Выберите 1 для Да или 2 для Нет.");    
                        break;
                }

                Console.WriteLine("\nВыберите наличие столовой (1 - Да, 2 - Нет):");
                ConsoleKeyInfo diningKey = Console.ReadKey(true);
                switch (diningKey.KeyChar)
                {
                    case '1':
                        passengerCarriage.HasDiningFacilities = true;
                        Console.WriteLine("\nСтоловая: Да");
                        break;
                    case '2':
                        passengerCarriage.HasDiningFacilities = false;
                        Console.WriteLine("\nСтоловая: Нет");
                        break;
                    default:
                        Console.WriteLine("\nНеверный выбор. Выберите 1 для Да или 2 для Нет.");
                        break;
                }

            }

            return passengerCarriage;
        }

        public override void CountPrice()
        {
            switch (Comfort)
            {
                case ComfortLevel.Economy:
                    FinalPrice = base.BaseTransportPrice * 0.5;
                    break;
                case ComfortLevel.Standard:
                    FinalPrice = base.BaseTransportPrice;
                    break;
                case ComfortLevel.Business:
                    FinalPrice = base.BaseTransportPrice * 2;
                    break;
                case ComfortLevel.Luxury:
                    FinalPrice = base.BaseTransportPrice * 3;
                    break;
                default:
                    FinalPrice += 50.0;
                    break;
            }

            FinalPrice += PassengerCount * 10.0;
            FinalPrice *= Distance / 100.0;
        }

        public override string ToString()
        {
            return $"Тип вагона: Passenger Carriage\n" +
                   $"ID вагона: {CarriageId}\n" +
                   $"Дистанция: {Distance} км\n" +
                   $"Тип комфорта: {Comfort}\n" +
                   $"Количество мест: {PassengerCapacity}\n" +
                   $"Занято мест: {PassengerCount}\n" +
                   $"Наличие Wi-Fi: {(HasWiFi ? "Да" : "Нет")}\n" +
                   $"Наличие столовой: {(HasDiningFacilities ? "Да" : "Нет")}\n" +
                   $"Итоговая цена: {FinalPrice:F2} BYN\n" +
                   $"Статус: {Status}"; ;
        }


        public int CompareTo(PassengerCarriage? other)
        {
            if (other == null) return 1;  
            return this.Comfort.CompareTo(other.Comfort); 
        }
    }
}
