using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab13 {
    public enum SerializationType {
        Binary,
        Soap,
        Xml,
        Json,
    }

    public static class Serializer {
        public static void Serialize(Stream serializationStream, Object obj, SerializationType type) {
            switch (type) {
                case SerializationType.Binary:
                    BinarySerializer.Serialize(serializationStream, obj);
                    return;
                case SerializationType.Soap:
                    SoapSerializer.Serialize(serializationStream, obj);
                    return;
                case SerializationType.Xml:
                    XmlSerializer.Serialize(serializationStream, obj);
                    return;
                case SerializationType.Json:
                    JsonSerializer.Serialize(serializationStream, obj); 
                    return;
            }
            throw new NotImplementedException($"Can't serialize object to '{type}'");
        }

        public static T Deserialize<T>(Stream serializationStream, SerializationType type) {
            switch (type) {
                case SerializationType.Binary:
                    return BinarySerializer.Deserialize<T>(serializationStream);
                case SerializationType.Soap:
                    return SoapSerializer.Deserialize<T>(serializationStream);
                case SerializationType.Xml:
                    return XmlSerializer.Deserialize<T>(serializationStream);
                case SerializationType.Json:
                    return JsonSerializer.Deserialize<T>(serializationStream);
            }
            throw new NotImplementedException($"Can't deserialize object from '{type}'");
        }
    }
}
