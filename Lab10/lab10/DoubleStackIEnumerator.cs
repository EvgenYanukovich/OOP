using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab02 {
    public partial class DoubleStack : IEnumerable {
        public IEnumerator GetEnumerator() {
            return new DoubleStackEnumerator(_storage);
        }

        private class DoubleStackEnumerator : IEnumerator {
            private List<double> _storage;
            private int _index = -1;

            public DoubleStackEnumerator(List<double> storage) {
                _storage = storage;
            }

            private IEnumerator GetEnumerator() {
                return (IEnumerator)this;
            }

            public bool MoveNext() {
                _index++;
                return (_index < _storage.Count);
            }
           
            public void Reset() {
                _index = -1;
            }
            
            public object Current {
                get => _storage[_index];
            }
        }
    }
}
