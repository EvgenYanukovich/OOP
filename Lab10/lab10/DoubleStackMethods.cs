using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace lab02 {
    public partial class DoubleStack {
        public void Push(double value) {
            _storage.Add(value);
        }

        public double Pop() {
            if (IsEmpty) {
                throw new InvalidOperationException("Can't execute 'Pop' method on empty stack");
            }
            int indexToDelete = _storage.Count - 1;
            double valueToDelete = _storage.ElementAt(indexToDelete);
            _storage.RemoveAt(indexToDelete);
            return valueToDelete;
        }

        public void Clear() {
            _storage.Clear();
        }

        public void SwapLast(ref double newValue, out double oldValue) {
            oldValue = Pop();
            Push(newValue);
        }

        public static string GetClassInfo() {
            return $"[class '{CLASS_NAME}' with " +
                $"{_currentInstanceCount} ({_totalInstanceCount} total) created instances, " +
                $"first creation at {_firstCreationTime}" +
                $"]";
        }

        public string GetInfo() {
            return $"[{CLASS_NAME}#{_id} '{_title}' instance created at {_creationTime}]";
        }

        public override string ToString() {
            return $"{{{string.Join(" ", _storage)}}}";
        }

        public override bool Equals(object obj) {
            if (!(obj is DoubleStack)) {
                return false;
            }
            return Equals((DoubleStack)obj);
        }

        public bool Equals(DoubleStack other) {
            return _storage.SequenceEqual(other._storage);
        }

        public override int GetHashCode() {
            int hashCode = -1774059772;
            hashCode = hashCode * -1521134295 + _id.GetHashCode();
            hashCode = hashCode * -1521134295 + _creationTime.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<double>>.Default.GetHashCode(_storage);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_title);
            return hashCode;
        }

        public static bool operator ==(DoubleStack left, DoubleStack right) {
            return EqualityComparer<DoubleStack>.Default.Equals(left, right);
        }

        public static bool operator !=(DoubleStack left, DoubleStack right) {
            return !(left == right);
        }
    }
}
