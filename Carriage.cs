using System;
using System.Xml.Serialization;

namespace Lab5
{
    public enum TypeOfCarriage
    {
        UNKNOWN,
        PASSENGER,
        BAGGAGE,
    }
    public enum CarriageStatus
    {
        Active,
        Inactive
    }


    [XmlInclude(typeof(PassengerCarriage))]
    [XmlInclude(typeof(BaggageCarriage))]
    public abstract class Carriage: IComparable<Carriage>
    {

        [XmlIgnore]
        private int carriageId;

        [XmlIgnore]
        private double baseTransportPrice; // Переименовано поле

        [XmlIgnore]
        private double distance;
        [XmlIgnore]
        private Random random = new Random();

        [XmlIgnore]
        public CarriageStatus Status { get; set; } = CarriageStatus.Active;

        [XmlElement("CarriageId")]
        public int CarriageId
        {
            get { return carriageId; }
            set { carriageId = value; }
        }


        [XmlElement("BaseTransportPrice")]
        public double BaseTransportPrice 
        {
            get { return baseTransportPrice; }
            set { baseTransportPrice = value; }
        }


        [XmlElement("Distance")]
        public double Distance
        {
            get { return distance; }
            set 
            {
                if (value >= 100 || value <= 3000)
                {
                    distance = value;
                }
                else
                {
                    distance = 0;
                }
            }
        }
        public static void SetBaseCarriageFields(Carriage carriage)
        {
            carriage.CarriageId = carriage.random.Next(0, 10000);
            string formattedId = carriage.CarriageId.ToString("D4");
            Console.WriteLine("Carriage ID: " + formattedId + "\n");

            Console.Write("Введите расстояние поездки (100-3000 км):");
            while (true)
            {
                carriage.Distance = Convert.ToDouble(Console.ReadLine());
                if (carriage.Distance < 100 || carriage.Distance > 3000)
                {
                    Console.Write("Ошибка некорректный ввод:");
                    continue;
                }

                Console.WriteLine("");
                break;
            }

            Console.Write("Введите базовую цену билета:");
            carriage.BaseTransportPrice = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("");
        }

        public Carriage() { }
        public Carriage(int carriageId, double distance,double baseTransportPrice) 
        {
            CarriageId = carriageId;
            Distance = distance;
            BaseTransportPrice = 0.0;
        }
        public int CompareTo(Carriage? other)
        {
            if (other == null) return 1;
            return this.Distance.CompareTo(other.Distance);
        }

        public abstract void CountPrice();
    }
}
