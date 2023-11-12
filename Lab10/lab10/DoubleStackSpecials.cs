using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab02 {
    public partial class DoubleStack {
        static DoubleStack() {
            Debug.WriteLine("Static constructor is called");
            _totalInstanceCount = 0;
            _currentInstanceCount = 0;
            _firstCreationTime = DateTime.Now;
        }

        private DoubleStack(List<double> storage, string title = "") {
            Debug.WriteLine("Private constructor is called");
            _totalInstanceCount++;
            _currentInstanceCount++;

            _id = _totalInstanceCount;
            _creationTime = DateTime.Now;
            this._storage = storage;

            if (string.IsNullOrWhiteSpace(title)) {
                _title = $"{CLASS_NAME}#{_id}";
            }
            else {
                _title = title;
            }
        }

        public DoubleStack()
            : this(new List<double>()) {
            Debug.WriteLine("Public constructor without arguments is called");
        }

        public DoubleStack(int capacity)
            : this(new List<double>(capacity)) {
            Debug.WriteLine("Public constructor with arguments #1 is called");
        }

        public DoubleStack(string title)
            : this(new List<double>(), title) {
            Debug.WriteLine("Public constructor with arguments #2 is called");
        }

        public DoubleStack(int capacity = 15, string title = "")
            : this(new List<double>(capacity), title) {
            Debug.WriteLine("Public constructor with arguments by default #1 is called");
        }

        public DoubleStack(IEnumerable<double> storage, string title = "")
            : this(new List<double>(storage), title) {
            Debug.WriteLine("Public constructor with arguments by default #2 is called");
        }

        public DoubleStack(DoubleStack oldStack)
            : this(new List<double>(oldStack._storage), oldStack._title) {
            Debug.WriteLine("Copy constructor is called");
        }

        ~DoubleStack() {
            Debug.WriteLine("Finalizer is called");
            _currentInstanceCount--;
        }
    }
}
