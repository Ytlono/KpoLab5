using System;
using System.Xml.Serialization;

namespace Lab5
{
    public class BaggageCarriage : Carriage
    {

        [XmlIgnore]
        private int baggageCapacity;

        [XmlIgnore]
        private int baggageCount;    // Текущее количество багажа

        [XmlIgnore]
        private double totalBaggageWeight; // Общий вес багажа (в кг)

        [XmlIgnore]
        private double pricePerKg;   // Стоимость перевозки одного килограмма багажа

        [XmlIgnore]
        private double finalPrice;   // Итоговая стоимость


        [XmlElement("BaggageCapacity")]
        public int BaggageCapacity
        {
            get { return baggageCapacity; }
            set { baggageCapacity = value; }
        }


        [XmlElement("BaggageCount")]
        public int BaggageCount
        {
            get { return baggageCount; }
            set { baggageCount = value; }
        }

        [XmlElement("TotalBaggageWeight")]
        public double TotalBaggageWeight
        {
            get { return totalBaggageWeight; }
            set { totalBaggageWeight = value; }
        }

        [XmlElement("PricePerKg")]
        public double PricePerKg
        {
            get { return pricePerKg; }
            set { pricePerKg = value; }
        }

        [XmlElement("FinalPrice")]
        public double FinalPrice
        {
            get { return finalPrice; }
            set { finalPrice = value; }
        }

        public BaggageCarriage() { }

        public BaggageCarriage(int carriageId, double distance, double baseTransportPrice, int baggageCapacity, double pricePerKg)
            : base(carriageId, distance, baseTransportPrice)
        {
            BaggageCapacity = baggageCapacity;
            PricePerKg = pricePerKg;
        }

        public static BaggageCarriage Set()
        {
            BaggageCarriage baggageCarriage = new BaggageCarriage(0, 0.0, 0.0, 0, 0);

            Carriage.SetBaseCarriageFields(baggageCarriage);

            Console.Write("Введите максимальную вместимость багажа (в штуках): ");
            baggageCarriage.BaggageCapacity = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите стоимость перевозки 1 кг багажа: ");
            baggageCarriage.PricePerKg = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите текущее количество багажа (не больше вместимости): ");
            while (true)
            {
                int bagCarCap = Convert.ToInt32(Console.ReadLine());
                if (bagCarCap >= 0 && bagCarCap <= baggageCarriage.BaggageCapacity)
                {
                    baggageCarriage.BaggageCount = bagCarCap;
                    break;
                }
                Console.Write("Ошибка, введите корректное количество: ");
            }

            Console.Write("Введите общий вес багажа (в кг): ");
            baggageCarriage.TotalBaggageWeight = Convert.ToDouble(Console.ReadLine());

            return baggageCarriage;
        }


        public override void CountPrice()
        {
            FinalPrice = (BaseTransportPrice + TotalBaggageWeight * PricePerKg) * (Distance / 100.0);
        }

        public override string ToString()
        {
            return $"Baggage Carriage ID: {CarriageId}\n" +
                   $"Distance: {Distance} км\n" +
                   $"Base Price: {BaseTransportPrice} BYN\n" +
                   $"Baggage Capacity: {BaggageCapacity} шт.\n" +
                   $"Baggage Count: {BaggageCount} шт.\n" +
                   $"Total Weight: {TotalBaggageWeight} кг\n" +
                   $"Price per kg: {PricePerKg}\n" +
                   $"Final Price: {FinalPrice:F2} BYN\n"+
                   $"Статус: {Status}";
        }
    }
}
