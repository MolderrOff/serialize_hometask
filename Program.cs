using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design.Serialization;
using System.Text.Json;
using System.Xml.Serialization;

namespace HomeTask_serialization
{
    public class Program
    {

        [XmlType("XML-CarCharacteristics")]
        public class Car
        {
            //public DateTime Time { get; set; }
            public string Model { get; set; }
            public int ModelCode { get; set; }
            [XmlElement("MaxSpeed")]
            public double Speed { get; set; }
            public required string Color { get; set; }

        }
        [XmlRoot("CarDescription")]
        public class CarInfo
        {
            [XmlElement("Reference")]
            public Car Current { get; set; }
            public List<Car> Journal { get; set; }
        }

        static void Main(string[] args)
        // Напишите приложение, конвертирующее произвольный JSON в XML.
        {
            var fileJson = "{\"Current\":" +
                "{\"Model\":\"BMW\",\"ModelCode\":4,\"Speed\":240,\"Color\":\"red\"}," +
                "\"Journal\":" +
                "[{\"Model\":\"Audi\",\"ModelCode\":5,\"Speed\":150,\"Color\":\"green\"}," +
                "{\"Model\":\"VAZ\",\"ModelCode\":3,\"Speed\":120,\"Color\":\"blue\"}," +
                "{\"Model\":\"Infinity\",\"ModelCode\":10,\"Speed\":190,\"Color\":\"black\"}]}";



            var carInfo = JsonSerializer.Deserialize<CarInfo>(fileJson);
            Console.WriteLine("Исходный формат JSON: ");
            Console.WriteLine();
            Console.WriteLine(JsonSerializer.Serialize(carInfo, new JsonSerializerOptions { WriteIndented = true }));
            Console.WriteLine("------------------------------");
            Console.WriteLine("Формат XML:  ");
            Console.WriteLine();


            if (carInfo.Current.Model == "VAZ")
            {
                carInfo.Current.Model = "Lada";
            }
            
            for (int i = 0; i < 2; i++)
            {
                if (carInfo.Journal[i].Model == "VAZ")
                {
                    carInfo.Journal[i].Model = "Lada";
                }
            }

            var serializer = new XmlSerializer(typeof(CarInfo));
            serializer.Serialize(Console.Out, carInfo);
            Console.WriteLine();
            Console.WriteLine("------------------------------");
            Console.WriteLine("Конвертация JSON в XML с учётом региональных стандартов закончена успешно");


        }

    }

}