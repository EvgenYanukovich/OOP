using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace lab13 {
    public class BinarySerializer : ISerializer {
        static private BinaryFormatter _formatter = new BinaryFormatter();

        public static void Serialize(Stream serializationStream, Object obj) {
            try {
#pragma warning disable SYSLIB0011
                _formatter.Serialize(serializationStream, obj);
#pragma warning restore SYSLIB0011
            }
            catch (Exception ex) {
                throw new SerializationException($"{ex.GetType().Name} while serialization: {ex.Message}");
            }
        }

        public static T Deserialize<T>(Stream serializationStream) {
            try {
#pragma warning disable SYSLIB0011
                return (T)_formatter.Deserialize(serializationStream);
#pragma warning restore SYSLIB0011
            }
            catch (Exception ex) {
                throw new SerializationException($"{ex.GetType().Name} while deserialization: {ex.Message}");
            }
        }
    }
}
