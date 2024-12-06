using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab5
{
    public class TrainSerializer
    {
        public  Train DeserializeTrain(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не найден. Создается новый поезд.");
                return new Train();
            }

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Train));
                    Console.WriteLine($"Десереализация выполнена успешно");
                    return (Train)serializer.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при десериализации: {ex.Message}");
                return new Train();
            }

        }

        public  void SerializeTrain(Train train, string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Train));
                    serializer.Serialize(fs, train);
                    Console.WriteLine($"Поезд успешно сохранен в файл {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сериализации: {ex.Message}");
            }
        }
    }
}