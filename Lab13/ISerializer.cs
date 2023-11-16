using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab13 {
    public interface ISerializer {
        static abstract void Serialize(Stream serializationStream, Object obj);

        static abstract T Deserialize<T>(Stream serializationStream);
    }

    public class SerializationException : Exception {
        public SerializationException(string message) : base(message) { }
    }
}
