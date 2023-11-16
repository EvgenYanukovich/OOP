using System.Xml;

namespace lab13 {
    internal class Program {
        static readonly string LAB_PATH = @"D:\Education\2 курс\Семестр 3\Объектно-ориентированное программирование\lab13";
        static readonly string BIN_FILE = Path.Combine(LAB_PATH, "serialized", "serialized.bin");
        static readonly string SOAP_FILE = Path.Combine(LAB_PATH, "serialized", "serialized.soap");
        static readonly string XML_FILE = Path.Combine(LAB_PATH, "serialized", "serialized.xml");
        static readonly string JSON_FILE = Path.Combine(LAB_PATH, "serialized", "serialized.json");

        static void Main(string[] args) {
            Furniture furniture = new();
            furniture.value = 1;

            Console.WriteLine("\tBefore Serialization");
            Console.WriteLine($"furniture: {furniture}");
            Console.WriteLine($"furniture.value: '{furniture.value}'");

            Serialize(furniture);
            Deserialize();
        }

        static void Serialize(Furniture furniture) {
            using (var fs = new FileStream(BIN_FILE, FileMode.OpenOrCreate)) {
                Serializer.Serialize(fs, furniture, SerializationType.Binary);
            }

            using (var fs = new FileStream(SOAP_FILE, FileMode.OpenOrCreate)) {
                Serializer.Serialize(fs, furniture, SerializationType.Soap);
            }

            using (var fs = new FileStream(XML_FILE, FileMode.OpenOrCreate)) {
                Serializer.Serialize(fs, furniture, SerializationType.Xml);
            }

            using (var fs = new FileStream(JSON_FILE, FileMode.OpenOrCreate)) {
                Serializer.Serialize(fs, furniture, SerializationType.Json);
            }
        }

        static void Deserialize() {
            Furniture furnitureFromBin;
            Furniture furnitureFromSoap;
            Furniture furnitureFromXml;
            Furniture furnitureFromJson;

            using (var fs = new FileStream(BIN_FILE, FileMode.OpenOrCreate)) {
                furnitureFromBin = Serializer.Deserialize<Furniture>(fs, SerializationType.Binary);
            }

            using (var fs = new FileStream(SOAP_FILE, FileMode.OpenOrCreate)) {
                furnitureFromSoap = Serializer.Deserialize<Furniture>(fs, SerializationType.Soap);
            }

            using (var fs = new FileStream(XML_FILE, FileMode.OpenOrCreate)) {
                furnitureFromXml = Serializer.Deserialize<Furniture>(fs, SerializationType.Xml);
            }

            using (var fs = new FileStream(JSON_FILE, FileMode.OpenOrCreate)) {
                furnitureFromJson = Serializer.Deserialize<Furniture>(fs, SerializationType.Json);
            }

            Console.WriteLine("\tBinary");
            Console.WriteLine($"furnitureFromBin: {furnitureFromBin}");
            Console.WriteLine($"furnitureFromBin.value: '{furnitureFromBin.value}'");

            Console.WriteLine("\tSoap");
            Console.WriteLine($"furnitureFromSoap: {furnitureFromSoap}");
            Console.WriteLine($"furnitureFromSoap.value: '{furnitureFromSoap.value}'");

            Console.WriteLine("\tXml");
            Console.WriteLine($"furnitureFromXml: {furnitureFromXml}");
            Console.WriteLine($"furnitureFromXml.value: '{furnitureFromXml.value}'");

            Console.WriteLine("\tJson");
            Console.WriteLine($"furnitureFromJson: {furnitureFromJson}");
            Console.WriteLine($"furnitureFromJson.value: '{furnitureFromJson.value}'");
        }
    }
}