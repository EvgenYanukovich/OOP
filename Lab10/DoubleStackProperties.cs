using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab02 {
    public partial class DoubleStack {
        public static int TotalInstanceCount {
            get => _totalInstanceCount;
        }

        public static int CurrentInstanceCount {
            get => _currentInstanceCount;
        }

        public static DateTime FirstCreationTime { 
            get => _firstCreationTime; 
        }

        public int Id {
            get => _id;
        }

        public DateTime CreationTime {
            get => _creationTime;
        }

        public List<double> Storage {
            get => _storage;
        }

        public string Title {
            get => _title;
            set => _title = value;
        }

        public int Capacity {
            get => _storage.Capacity;
            set => _storage.Capacity = value;
        }

        public int Count {
            get => _storage.Count;
        }

        public bool IsEmpty {
            get => _storage.Count == 0;
        }

        public double Top {
            get => _storage.Last();
        }

        public double this[int index] {
            get => _storage[index];
        }
    }
}
