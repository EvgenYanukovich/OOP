using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemXmlSerializer = System.Xml.Serialization.XmlSerializer;

namespace lab13 {
    public class XmlSerializer : ISerializer {
        public static void Serialize(Stream serializationStream, Object obj) {
            SystemXmlSerializer serializer = new SystemXmlSerializer(obj.GetType());
            try {
                serializer.Serialize(serializationStream, obj);
            }
            catch (Exception ex) { 
                throw new SerializationException($"{ex.GetType().Name} while serialization: {ex.Message}");
            }
        }

        public static T Deserialize<T>(Stream serializationStream) {
            SystemXmlSerializer serializer = new SystemXmlSerializer(typeof(T));
            T? deserialized = (T?)serializer.Deserialize(serializationStream);
            if (deserialized == null) {
                throw new SerializationException($"Can't deserialize object from xml");
            }
            return deserialized;
        }
    }
}
