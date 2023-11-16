using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemJsonSerializer = System.Text.Json.JsonSerializer;

namespace lab13 {
    public class JsonSerializer : ISerializer {
        public static void Serialize(Stream serializationStream, Object obj) {
            try {
                SystemJsonSerializer.Serialize(serializationStream, obj);
            }
            catch (Exception ex) {
                throw new SerializationException($"{ex.GetType().Name} while serialization: {ex.Message}");
            }
        }

        public static T Deserialize<T>(Stream serializationStream) {
            try {
                T? deserialized = SystemJsonSerializer.Deserialize<T>(serializationStream);
                if (deserialized == null) {
                    throw new SerializationException($"Can't deserialize object from json");
                }
                return deserialized;
            }
            catch (Exception ex) {
                throw new SerializationException($"{ex.GetType().Name} while deserialization: {ex.Message}");
            }
        }
    }
}
