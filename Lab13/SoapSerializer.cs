using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Soap;

namespace lab13 {
    public class SoapSerializer : ISerializer {
        private static SoapFormatter _formatter = new SoapFormatter();

        public static void Serialize(Stream serializationStream, Object obj) {
            try {
                _formatter.Serialize(serializationStream, obj);
            }
            catch (Exception ex) {
                throw new SerializationException($"{ex.GetType().Name} while serialization: {ex.Message}");
            }
        }

        public static T Deserialize<T>(Stream serializationStream) {
            try {
                return (T)_formatter.Deserialize(serializationStream);
            }
            catch (Exception ex) {
                throw new SerializationException($"{ex.GetType().Name} while deserialization: {ex.Message}");
            }
        }
    }
}
